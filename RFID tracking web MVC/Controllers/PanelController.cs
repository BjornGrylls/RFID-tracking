using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RFID_tracking_web_MVC.Models;
using System.Runtime.CompilerServices;
using static System.Net.WebRequestMethods;

namespace RFID_tracking_web_MVC.Controllers {
    public class PanelController : Controller {

        private readonly IMemoryCache _memoryCache;

        public PanelController(IMemoryCache memoryCache) {
            _memoryCache = memoryCache;
        }

        public IActionResult Home() {

            HttpContext.Response.Headers.Add("refresh", "2; url=" + Url.Action("Home")); // Muy dirty

            RestClient restClient = new RestClient("https://rfidtrackingapi20230502171130.azurewebsites.net", new HttpClient());
            // restClient.BaseUrl = "https://localhost:7098";
            var json = restClient.TagPacketGETAsync().Result;
            var scannedWeapons = JsonConvert.DeserializeObject<TagPacket>(json);

            // Cache
            var weapons = _memoryCache.GetOrCreate(
                "Weapons",
                cacheEntry => {
                    cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
                    return restClient.WeaponsAllAsync().Result;

                });
            //ViewBag.Weapons = weapons;

            // Cache
            var activeLoans = _memoryCache.GetOrCreate(
                "ActiveLoans",
                cacheEntry => {
                    cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
                    return restClient.LoansAllAsync().Result.Where(x => x.LoanEnd < x.LoanStart);
                }).ToList();
            //ViewBag.ActiveLoans = activeLoans;

            // Cache
            var activeShooters = _memoryCache.GetOrCreate(
                "ActiveShooters",
                cacheEntry => {
                    cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
                    return restClient.ShootersAllAsync().Result.Where(x => activeLoans.Any(y => y.ShooterId == x.Id));
                });
            //ViewBag.ActiveShooters = activeShooters;

            PanelHomeViewModel panelHomeViewModel = new PanelHomeViewModel { list = new List<PanelHomeElement>() };

            foreach (var tag in scannedWeapons.tags) {
                Weapon weapon;
                try {
                    weapon = weapons.First(weapon => weapon.RfidTag == tag.EPC96);
                } catch (Exception) {
                    weapon = new Weapon { FriendlyName = "Ukendt!", Status = WeaponStatus._0 }; // Unknown RFID tag
                }

                // Er der et udlån at finde?
                if (weapon.Status == WeaponStatus._2) {
                    Loan loan = activeLoans.First(loan => loan.WeaponId == weapon.Id);
                    Shooter shooter = activeShooters.First(shooter => shooter.Id == loan.ShooterId);

                    panelHomeViewModel.list.Add(new PanelHomeElement {
                        WeaponName = weapon.FriendlyName,
                        ShooterName = shooter.Name,
                        WeaponStatus = weapon.Status,
                        WeaponId = weapon.Id,
                    });
                } else {
                    panelHomeViewModel.list.Add(new PanelHomeElement {
                        WeaponName = weapon.FriendlyName,
                        WeaponStatus = weapon.Status,
                        WeaponId = weapon.Id,
                    });
                }
            }

            return View(panelHomeViewModel);
        }

        [HttpGet("List/{inSafe:bool}")]
        public IActionResult List(bool inSafe) {

            // Cache
            var weapons = _memoryCache.GetOrCreate(
                "Weapons" + inSafe,
                cacheEntry => {
                    cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
                    RestClient restClient = new RestClient("https://rfidtrackingapi20230502171130.azurewebsites.net", new HttpClient());
                    var allWeapons = restClient.WeaponsAllAsync().Result;

                    if (inSafe) {
                        return allWeapons.Where(x => x.Status == WeaponStatus._1); // _1 = i boksen
                    } else {
                        return allWeapons.Where(x => x.Status != WeaponStatus._1);
                    }

                });

            ViewData["Title"] = inSafe ? "På vej ud af boksen" : "På vej ind i boksen";
            ViewBag.inSafe = inSafe;

            return View(weapons);
        }

        [HttpGet("SpecifyUser/{weaponId:Guid}")]
        public IActionResult SpecifyUser(Guid weaponId) {
            RestClient restClient = new RestClient("https://rfidtrackingapi20230502171130.azurewebsites.net", new HttpClient());
            var weapon = restClient.WeaponsGETAsync(weaponId).Result;

            // Cache
            var regularUsers = _memoryCache.GetOrCreate(
                "RegularUsers" + weaponId,
                cacheEntry => {
                    cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
                    var regularUsers = restClient.RegularUsersAllAsync().Result.Where(x => x.WeaponId == weaponId);
                    var shooters = restClient.ShootersAllAsync().Result;

                    var list = new List<RegularUsersViewModel>();
                    foreach (var item in regularUsers) {
                        list.Add(new RegularUsersViewModel {
                            Weapon = weapon,
                            Shooter = shooters.Where(x => x.Id == item.UserId).First()
                        });
                    }

                    return list;
                });

            ViewBag.Weapon = weapon;

            return View(regularUsers);
        }

        [HttpGet("Confirmation")]
        public IActionResult Confirmation(Guid userId, Guid weaponId) {
            RestClient restClient = new RestClient("https://rfidtrackingapi20230502171130.azurewebsites.net", new HttpClient());
            var loans = restClient.LoansAllAsync().Result;
            var loansAlreadyCreated = loans.Where(x => x.WeaponId == weaponId && x.ShooterId == userId && x.LoanEnd < x.LoanStart);
            bool loanAlreadyCreated = loansAlreadyCreated.Any();

            Weapon weapon = restClient.WeaponsGETAsync(weaponId).Result;

            Loan loan;
            bool inSafe;

            if (loanAlreadyCreated) {
                // Noget gik galt
                throw new Exception();
            }

            // Create new loan and give weapon
            loan = new Loan {
                Id = Guid.NewGuid(),
                ShooterId = userId,
                LoanStart = DateTime.Now,
                WeaponId = weaponId,
                LoanEnd = DateTime.Now.AddYears(-10)
            };
            var res = restClient.LoansPOSTAsync(loan);

            weapon.Status = WeaponStatus._2;
            inSafe = false;

            // Clear cache
            _memoryCache.Remove("Weapons");
            _memoryCache.Remove("Weapons" + true);
            _memoryCache.Remove("Weapons" + false);

            // Update weapon status
            restClient.WeaponsPUTAsync(weaponId, weapon).Wait();

            ViewBag.Weapon = weapon;
            ViewBag.Shooter = restClient.ShootersGETAsync(userId).Result;

            return View(loan);
        }

        [HttpGet("ConfirmationIn")]
        public IActionResult ConfirmationIn(Guid weaponId) {
            // Return weapon
            RestClient restClient = new RestClient("https://rfidtrackingapi20230502171130.azurewebsites.net", new HttpClient());
            var loans = restClient.LoansAllAsync().Result;
            var loansAlreadyCreated = loans.Where(x => x.WeaponId == weaponId && x.LoanEnd < x.LoanStart);
            bool loanAlreadyCreated = loansAlreadyCreated.Any();

            Weapon weapon = restClient.WeaponsGETAsync(weaponId).Result;

            Loan loan;
            bool inSafe;

            if (loanAlreadyCreated) {
                // Stop loan and return weapon
                loan = loansAlreadyCreated.First();
                loan.LoanEnd = DateTime.Now;
                restClient.LoansPUTAsync(loan.Id, loan).Wait();
            } else {
                loan = new Loan {
                    WeaponId = weapon.Id
                };
            }

            // Update weapon status
            weapon.Status = WeaponStatus._1;
            restClient.WeaponsPUTAsync(weaponId, weapon);

            // Clear cache
            _memoryCache.Remove("Weapons");
            _memoryCache.Remove("Weapons" + true);
            _memoryCache.Remove("Weapons" + false);

            ViewBag.Weapon = weapon;
            ViewBag.Shooter = new Shooter { Name = "Fejl" };

            return View("Confirmation", loan);
        }
    }
}

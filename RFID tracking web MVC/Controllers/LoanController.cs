using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RFID_tracking_web_MVC.Models;

namespace RFID_tracking_web_MVC.Controllers {
    public class LoanController : Controller {
        private readonly IMemoryCache _memoryCache;

        public LoanController(IMemoryCache memoryCache) {
            _memoryCache = memoryCache;
        }
        public IActionResult Index() {
            RestClient restClient = new RestClient("https://rfidtrackingapi20230502171130.azurewebsites.net", new HttpClient());

            // Cache
            var activeLoans = _memoryCache.GetOrCreate(
                "Loans",
                cacheEntry => {
                    cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
                    return restClient.LoansAllAsync().Result.Where(x => x.LoanEnd < x.LoanStart);
                });

            // Cache
            var weapons = _memoryCache.GetOrCreate(
                "Weapons",
                cacheEntry => {
                    cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
                    return restClient.WeaponsAllAsync().Result;
                });

            // Cache
            var shooters = _memoryCache.GetOrCreate(
                "Shooters",
                cacheEntry => {
                    cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
                    return restClient.ShootersAllAsync().Result;
                });

            // Cache
            var directors = _memoryCache.GetOrCreate(
                "Directors",
                cacheEntry => {
                    cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
                    return restClient.DirectorsAllAsync().Result;
                });

            var model = new List<ActiveLoansViewModel>();
            foreach (var loan in activeLoans) {
                Weapon weapon;
                try {
                    weapon = weapons.First(weapon => weapon.Id == loan.WeaponId);
                } catch (Exception) {
                    weapon = new Weapon { FriendlyName = "Could not find weapon", RegistrationNumber = "" };
                }

                Shooter shooter;
                Director director;
                try {
                    shooter = shooters.First(shooter => shooter.Id == loan.ShooterId);
                    if (shooter.IsPictureIdShown) {
                        director = directors.First(director => director.Id == shooter.DirectorAcceptedPictureId);
                    } else {
                        director = new Director { Name = "" };
                    }
                } catch (Exception) {
                    shooter = new Shooter { Name = "Could not find shooter", Address = "", Birthday = DateTime.Now, IsPictureIdShown = false };
                    director = new Director { Name = "" };
                }

                model.Add(new ActiveLoansViewModel {
                    LoanStart = loan.LoanStart.DateTime,
                    WeaponName = weapon.FriendlyName,
                    WeaponRegId = weapon.RegistrationNumber,
                    ShooterName = shooter.Name,
                    ShooterAddress = shooter.Address,
                    ShooterBirthday = shooter.Birthday.DateTime,
                    ShooterShownId = shooter.IsPictureIdShown,
                    NameOfDirectorWhoAcceptedId = director.Name
                });
            }

            return View(model);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace RFID_tracking_web_MVC.Controllers {
    [Route("Admin/[controller]")]
    public class WeaponController : Controller {
        private readonly IMemoryCache _memoryCache;
        public WeaponController(IMemoryCache memoryCache) {
            _memoryCache = memoryCache;
        }

        // GET: WeaponController
        [HttpGet] 
        public ActionResult Index() {
            // Cache
            var weapons = _memoryCache.GetOrCreate(
                "Weapons",
                cacheEntry => {
                    cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
                    RestClient restClient = new RestClient("https://rfidtrackingapi20230502171130.azurewebsites.net", new HttpClient());
                    return restClient.WeaponsAllAsync().Result;
                });

            return View(weapons);
        }

        // GET: WeaponController/Details/5
        [HttpGet("Details/{id:Guid}")] 
        public ActionResult Details(Guid id) {
            RestClient restClient = new RestClient("https://rfidtrackingapi20230502171130.azurewebsites.net", new HttpClient());
            var weapon = restClient.WeaponsGETAsync(id).Result;
            return View(weapon);
        }

        // GET: WeaponController/Create
        [HttpGet("Create")] 
        public ActionResult Create() {
            return View();
        }

        // POST: WeaponController/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection) {
            collection.TryGetValue("FriendlyName", out var FriendlyName);
            collection.TryGetValue("RegistrationNumber", out var RegistrationNumber);
            collection.TryGetValue("RfidTag", out var RfidTag);
            collection.TryGetValue("Status", out var Status);
            Enum.TryParse<WeaponStatus>(Status, out var WeaponStatus);


            RestClient restClient = new RestClient("https://rfidtrackingapi20230502171130.azurewebsites.net", new HttpClient());
            restClient.WeaponsPOSTAsync(new Weapon {
                Id = Guid.NewGuid(),
                FriendlyName = FriendlyName,
                RegistrationNumber = RegistrationNumber,
                RfidTag = RfidTag,
                Status = WeaponStatus
            });

            // Clear cache
            _memoryCache.Remove("Weapons");

            try {
                return RedirectToAction(nameof(Index));
            } catch {
                return View();
            }
        }

        // GET: WeaponController/Edit/5
        [HttpGet("Edit/{id:Guid}")]
        public ActionResult Edit(Guid id) {
            RestClient restClient = new RestClient("https://rfidtrackingapi20230502171130.azurewebsites.net", new HttpClient());
            var weapon = restClient.WeaponsGETAsync(id).Result;
            return View(weapon);
        }

        // POST: WeaponController/Edit/5
        [HttpPost("Edit/{id:Guid}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection) {
            collection.TryGetValue("FriendlyName", out var FriendlyName);
            collection.TryGetValue("RegistrationNumber", out var RegistrationNumber);
            collection.TryGetValue("RfidTag", out var RfidTag);
            collection.TryGetValue("Status", out var Status);
            Enum.TryParse<WeaponStatus>(Status, out var WeaponStatus);


            RestClient restClient = new RestClient("https://rfidtrackingapi20230502171130.azurewebsites.net", new HttpClient());
            restClient.WeaponsPUTAsync(id,new Weapon {
                Id = id,
                FriendlyName = FriendlyName,
                RegistrationNumber = RegistrationNumber,
                RfidTag = RfidTag,
                Status = WeaponStatus
            });

            // Clear cache
            _memoryCache.Remove("Weapons");

            try {
                return RedirectToAction(nameof(Index));
            } catch {
                return View();
            }
        }

        // GET: WeaponController/Delete/5
        [HttpGet("Delete/{id:Guid}")]
        public ActionResult Delete(Guid id) {
            RestClient restClient = new RestClient("https://rfidtrackingapi20230502171130.azurewebsites.net", new HttpClient());
            var weapon = restClient.WeaponsGETAsync(id).Result;
            return View(weapon);
        }

        // POST: WeaponController/Delete/5
        [HttpPost("Delete/{id:Guid}")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection) {
            RestClient restClient = new RestClient("https://rfidtrackingapi20230502171130.azurewebsites.net", new HttpClient());
            restClient.WeaponsDELETEAsync(id);

            // Clear cache
            _memoryCache.Remove("Weapons");

            try {
                return RedirectToAction(nameof(Index));
            } catch {
                return View();
            }
        }
    }
}

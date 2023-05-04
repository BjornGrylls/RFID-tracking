using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace RFID_tracking_web_MVC.Controllers {
    [Route("admin/[controller]")]
    public class DirectorController : Controller {
        private readonly IMemoryCache _memoryCache;

        public DirectorController(IMemoryCache memoryCache) {
            _memoryCache = memoryCache;
        }

        [HttpGet] // GET: DirectorController
        public ActionResult Index() {

            // Cache
            var directors = _memoryCache.GetOrCreate(
                "Directors",
                cacheEntry => {
                    cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
                    RestClient restClient = new RestClient("https://rfidtrackingapi20230502171130.azurewebsites.net", new HttpClient());
                    return restClient.DirectorsAllAsync().Result;
                });

            return View(directors);
        }

        [HttpGet("Details/{id:Guid}")] // GET: DirectorController/Details/5
        public ActionResult Details(Guid id) {
            RestClient restClient = new RestClient("https://rfidtrackingapi20230502171130.azurewebsites.net", new HttpClient());
            var director = restClient.DirectorsGETAsync(id).Result;
            return View(director);
        }

        [HttpGet("Create")] // GET: DirectorController/Create
        public ActionResult Create() {
            return View();
        }

        // POST: DirectorController/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection) {
            collection.TryGetValue("Name", out var Name);
            collection.TryGetValue("MailAddress", out var MailAddress);
            collection.TryGetValue("PhoneNumber", out var PhoneNumber);

            RestClient restClient = new RestClient("https://rfidtrackingapi20230502171130.azurewebsites.net", new HttpClient());
            restClient.DirectorsPOSTAsync(new Director {
                Id = Guid.NewGuid(),
                Name = Name,
                MailAddress = MailAddress,
                PhoneNumber = PhoneNumber
            });

            try {
                return RedirectToAction(nameof(Index));
            } catch {
                return View();
            }
        }

        [HttpGet("Edit/{id:Guid}")] // GET: DirectorController/Edit/5
        public ActionResult Edit(Guid id) {
            return View();
        }

        // POST: DirectorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection) {
            try {
                return RedirectToAction(nameof(Index));
            } catch {
                return View();
            }
        }

        [HttpGet("Delete/{id:Guid}")] // GET: DirectorController/Delete/5
        public ActionResult Delete(Guid id) {
            return View();
        }

        // POST: DirectorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection) {
            try {
                return RedirectToAction(nameof(Index));
            } catch {
                return View();
            }
        }
    }
}

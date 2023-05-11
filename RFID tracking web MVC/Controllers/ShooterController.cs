using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Xml.Linq;

namespace RFID_tracking_web_MVC.Controllers {
    [Route("Admin/[controller]")]
    public class ShooterController : Controller {
        private readonly IMemoryCache _memoryCache;
        public ShooterController(IMemoryCache memoryCache) {
            _memoryCache = memoryCache;
        }

        // GET: ShooterController
        [HttpGet]
        public ActionResult Index() {
            RestClient restClient = new RestClient("https://rfidtrackingapi20230502171130.azurewebsites.net", new HttpClient());

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
            ViewBag.Directors = directors;

            return View(shooters);
        }

        // GET: ShooterController/Details/5
        [HttpGet("Details/{id:Guid}")]
        public ActionResult Details(Guid id) {
            RestClient restClient = new RestClient("https://rfidtrackingapi20230502171130.azurewebsites.net", new HttpClient());
            var shooter = restClient.ShootersGETAsync(id).Result;
            return View(shooter);
        }

        // GET: ShooterController/Create
        [HttpGet("Create")]
        public ActionResult Create() {
            // Cache
            var directors = _memoryCache.GetOrCreate(
                "Directors",
                cacheEntry => {
                    cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
                    RestClient restClient = new RestClient("https://rfidtrackingapi20230502171130.azurewebsites.net", new HttpClient());
                    return restClient.DirectorsAllAsync().Result;
                });

            ViewBag.Directors = directors.Select(x => new Itemlist {
                Value = x.Id,
                Text = x.Name
            }).ToList();

            return View();
        }

        // POST: ShooterController/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection) {
            collection.TryGetValue("Name", out var Name);
            collection.TryGetValue("MailAddress", out var mailAddress);
            collection.TryGetValue("ShooterId", out var ShooterId);
            int.TryParse(ShooterId, out var shooterId);
            collection.TryGetValue("Birthday", out var Birthday);
            DateTime.TryParse(Birthday, out var birthday);
            collection.TryGetValue("IsPictureIdShown", out var IsPictureIdShown);
            bool isPictureIdShown = IsPictureIdShown.Count == 2 ? true : false;
            collection.TryGetValue("DirectorAcceptedPictureId", out var DirectorAcceptedPictureId);
            Guid.TryParse(DirectorAcceptedPictureId, out var directorAcceptedPictureId);
            collection.TryGetValue("Address", out var Address);

            RestClient restClient = new RestClient("https://rfidtrackingapi20230502171130.azurewebsites.net", new HttpClient());
            restClient.ShootersPOSTAsync(new Shooter {
                Id = Guid.NewGuid(),
                Name = Name,
                MailAddress = mailAddress,
                ShooterId = shooterId,
                Birthday = birthday,
                IsPictureIdShown = isPictureIdShown,
                DirectorAcceptedPictureId = directorAcceptedPictureId,
                Address = Address,
            });

            // Clear cache
            _memoryCache.Remove("Shooters");

            try {
                return RedirectToAction(nameof(Index));
            } catch {
                return View();
            }
        }

        // GET: ShooterController/Edit/5
        [HttpGet("Edit/{id:Guid}")]
        public ActionResult Edit(Guid id) {
            RestClient restClient = new RestClient("https://rfidtrackingapi20230502171130.azurewebsites.net", new HttpClient());
            var shooter = restClient.ShootersGETAsync(id).Result;

            // Cache
            var directors = _memoryCache.GetOrCreate(
                "Directors",
                cacheEntry => {
                    cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
                    RestClient restClient = new RestClient("https://rfidtrackingapi20230502171130.azurewebsites.net", new HttpClient());
                    return restClient.DirectorsAllAsync().Result;
                });

            ViewBag.Directors = directors.Select(x => new Itemlist {
                Value = x.Id,
                Text = x.Name
            }).ToList();


            return View(shooter);
        }

        // POST: ShooterController/Edit/5
        [HttpPost("Edit/{id:Guid}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection) {
            collection.TryGetValue("Name", out var Name);
            collection.TryGetValue("MailAddress", out var mailAddress);
            collection.TryGetValue("ShooterId", out var ShooterId);
            int.TryParse(ShooterId, out var shooterId);
            collection.TryGetValue("Birthday", out var Birthday);
            DateTime.TryParse(Birthday, out var birthday);
            collection.TryGetValue("IsPictureIdShown", out var IsPictureIdShown);
            bool isPictureIdShown = IsPictureIdShown.Count == 2 ? true : false;
            collection.TryGetValue("DirectorAcceptedPictureId", out var DirectorAcceptedPictureId);
            Guid.TryParse(DirectorAcceptedPictureId.First(), out var directorAcceptedPictureId);
            collection.TryGetValue("Address", out var Address);

            RestClient restClient = new RestClient("https://rfidtrackingapi20230502171130.azurewebsites.net", new HttpClient());
            restClient.ShootersPUTAsync(id, new Shooter {
                Id = id,
                Name = Name,
                MailAddress = mailAddress,
                ShooterId = shooterId,
                Birthday = birthday,
                IsPictureIdShown = isPictureIdShown,
                DirectorAcceptedPictureId = directorAcceptedPictureId,
                Address = Address,
            });

            // Clear cache
            _memoryCache.Remove("Shooters");

            try {
                return RedirectToAction(nameof(Index));
            } catch {
                return View();
            }
        }

        // GET: ShooterController/Delete/5
        [HttpGet("Delete/{id:Guid}")]
        public ActionResult Delete(Guid id) {
            RestClient restClient = new RestClient("https://rfidtrackingapi20230502171130.azurewebsites.net", new HttpClient());
            var shooter = restClient.ShootersGETAsync(id).Result;
            return View(shooter);
        }

        // POST: ShooterController/Delete/5
        [HttpPost("Delete/{id:Guid}")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection) {
            RestClient restClient = new RestClient("https://rfidtrackingapi20230502171130.azurewebsites.net", new HttpClient());
            restClient.ShootersDELETEAsync(id);

            // Clear cache
            _memoryCache.Remove("Shooters");

            try {
                return RedirectToAction(nameof(Index));
            } catch {
                return View();
            }
        }

        public class Itemlist {
            public string Text { get; set; }
            public Guid Value { get; set; }
        }

    }
}

﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace RFID_tracking_web_MVC.Controllers {
    [Route("Admin/[controller]")]
    public class DirectorController : Controller {
        private readonly IMemoryCache _memoryCache;

        public DirectorController(IMemoryCache memoryCache) {
            _memoryCache = memoryCache;
        }

        // GET: DirectorController
        [HttpGet]
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

        // GET: DirectorController/Details/5
        [HttpGet("Details/{id:Guid}")]
        public ActionResult Details(Guid id) {
            RestClient restClient = new RestClient("https://rfidtrackingapi20230502171130.azurewebsites.net", new HttpClient());
            var director = restClient.DirectorsGETAsync(id).Result;
            return View(director);
        }

        // GET: DirectorController/Create
        [HttpGet("Create")]
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

            // Clear cache
            _memoryCache.Remove("Directors");

            try {
                return RedirectToAction(nameof(Index));
            } catch {
                return View();
            }
        }

        // GET: DirectorController/Edit/5
        [HttpGet("Edit/{id:Guid}")]
        public ActionResult Edit(Guid id) {
            RestClient restClient = new RestClient("https://rfidtrackingapi20230502171130.azurewebsites.net", new HttpClient());
            var director = restClient.DirectorsGETAsync(id).Result;
            return View(director);
        }

        // POST: DirectorController/Edit/5
        [HttpPost("Edit/{id:Guid}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection) {
            collection.TryGetValue("Name", out var Name);
            collection.TryGetValue("MailAddress", out var MailAddress);
            collection.TryGetValue("PhoneNumber", out var PhoneNumber);

            RestClient restClient = new RestClient("https://rfidtrackingapi20230502171130.azurewebsites.net", new HttpClient());
            restClient.DirectorsPUTAsync(id, new Director {
                Id = id,
                Name = Name,
                MailAddress = MailAddress,
                PhoneNumber = PhoneNumber
            });

            // Clear cache
            _memoryCache.Remove("Directors");

            try {
                return RedirectToAction(nameof(Index));
            } catch {
                return View();
            }
        }

        // GET: DirectorController/Delete/5
        [HttpGet("Delete/{id:Guid}")]
        public ActionResult Delete(Guid id) {
            RestClient restClient = new RestClient("https://rfidtrackingapi20230502171130.azurewebsites.net", new HttpClient());
            var director = restClient.DirectorsGETAsync(id).Result;
            return View(director);
        }

        // POST: DirectorController/Delete/5
        [HttpPost("Delete/{id:Guid}")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection) {
            RestClient restClient = new RestClient("https://rfidtrackingapi20230502171130.azurewebsites.net", new HttpClient());
            restClient.DirectorsDELETEAsync(id);

            // Clear cache
            _memoryCache.Remove("Directors");

            try {
                return RedirectToAction(nameof(Index));
            } catch {
                return View();
            }
        }
    }
}

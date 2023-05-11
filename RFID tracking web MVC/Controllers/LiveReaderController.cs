using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RFID_tracking_web_MVC.Models;

namespace RFID_tracking_web_MVC.Controllers {
    public class LiveReaderController : Controller {
        public IActionResult Index() {
            var res = Table() as PartialViewResult;
            var tables = res.Model as IEnumerable<Tag>;
            return View(tables);
        }
        public IActionResult Table() {

            RestClient restClient = new RestClient("https://rfidtrackingapi20230502171130.azurewebsites.net", new HttpClient());
            var json = restClient.TagPacketGETAsync().Result;
            var scannedTags = JsonConvert.DeserializeObject<TagPacket>(json);


            return PartialView("Table", scannedTags.tags);

        }
    }
}

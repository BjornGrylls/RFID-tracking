using Microsoft.AspNetCore.Mvc;

namespace RFID_tracking_web_MVC.Controllers {
    public class AdminController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RFID_tracking_web.Pages {
    public class IndexModel : PageModel {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger) {
            _logger = logger;
        }

        public void OnGet() {
            RestClient restClient = new RestClient("https://rfidtrackingapi20230502171130.azurewebsites.net/api/", new HttpClient());
            var weapons = restClient.WeaponsAllAsync().Result;
            
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Protocol;
using RFID_tracking_API.Models;
using System.Text.RegularExpressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RFID_tracking_API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TagPacketController : ControllerBase {
        private static List<Tag> Tags { get; set; }

        public TagPacketController() {
            if (Tags == null) { Tags = new List<Tag>(); }
        }


        // GET: api/<TagPacketController>
        [HttpGet]
        [Produces("application/json")] // Vigtig!!! Ellers forstår klienten ikke data
        public string Get() {
            return JObject.FromObject(new TagPacket { tags = Tags }).ToString(); //JsonConvert.SerializeObject(Tags);
        }

        // GET api/<TagPacketController>/5
        [HttpGet("{id}")]
        [Produces("application/json")] // Vigtig!!! Ellers forstår klienten ikke data
        public string Get(string id) {
            return JObject.FromObject(Tags.Where(x => x.EPC96 == id).First()).ToString(); // Returns tag by EPC-96
        }

        // POST api/<TagPacketController>
        [HttpPost]
        public void Post([FromBody] string value) {
            string formattedBody = value; // Format input with regex
            formattedBody = Regex.Replace(formattedBody, "u'", "'");
            formattedBody = Regex.Replace(formattedBody, "\\(", "");
            formattedBody = Regex.Replace(formattedBody, "L,\\)", "");
            formattedBody = Regex.Replace(formattedBody, ",\\)", "");

            var tagPacket = JsonConvert.DeserializeObject<List<Tag>>(formattedBody);

            Tags.AddRange(tagPacket);

            // Keep 20 newest readings
            Tags = Tags.OrderByDescending(x => x.LastSeenTimestampUTC).ToList();
            if (Tags.Count > 20) { Tags.RemoveRange(0, Tags.Count - 20); }

            Tags = Tags.DistinctBy(x => x.EPC96).ToList(); // Remove duplicates
            Tags = Tags.OrderByDescending(x => x.PeakRSSI).ToList(); // Sort by closest tag
        }

    }
}

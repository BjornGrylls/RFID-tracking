using Newtonsoft.Json;

namespace RFID_tracking_web_MVC.Models {

    public class TagPacket {
        public List<Tag> tags { get; set; }
    }

    public class Tag {
        public int SpecIndex { get; set; }
        public int ROSpecID { get; set; }

        [JsonProperty("EPC-96")]
        public string EPC96 { get; set; }
        public int ChannelIndex { get; set; }
        public int PeakRSSI { get; set; }
        public int AntennaID { get; set; }
        public object FirstSeenTimestampUTC { get; set; } // Microseconds epoch
        public int TagSeenCount { get; set; }
        public object LastSeenTimestampUTC { get; set; } // Microseconds epoch
        public int AccessSpecID { get; set; }
        public int InventoryParameterSpecID { get; set; }
    }
}

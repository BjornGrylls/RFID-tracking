using RFID_tracking_API.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Composition.Convention;
using System.Text.Json.Serialization;

namespace RFID_tracking_API.Data {
    public class Shooter : Profile {

        public int ShooterId { get; set; }

        public DateTime Birthday { get; set; }

        [Required]
        public bool IsPictureIdShown { get; set; }

        [ForeignKey("Director")]
        public Guid DirectorAcceptedPictureId { get; set; }

        public string Address { get; set; }

    }
}

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RFID_tracking_API.Data {
    public class Weapon {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string RegistrationNumber { get; set; }

        [Required]
        public string FriendlyName { get; set; }

        public string RfidTag { get; set; }

        [Required]
        public WeaponStatus Status { get; set; }
    }

    public enum WeaponStatus {
        Unknown,
        In,
        Out,
        PermitGenerated
    }
}

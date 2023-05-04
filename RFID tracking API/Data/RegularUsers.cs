using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RFID_tracking_API.Data {
    public class RegularUsers {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey("Weapon")]
        public Guid WeaponId { get; set; }

        [Required]
        [ForeignKey("Shooter")]
        public Guid UserId { get; set; }
    }

}

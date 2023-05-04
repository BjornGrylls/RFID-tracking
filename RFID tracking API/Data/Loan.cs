using RFID_tracking_API.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Composition.Convention;

namespace RFID_tracking_API.Data {
    public class Loan {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey("Shooter")]
        public Guid ShooterId { get; set; }

        [Required]
        [ForeignKey("Weapon")]
        public Guid WeaponId { get; set; }

        [Required]
        public DateTime LoanStart { get; set; }

        public DateTime LoanEnd { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RFID_tracking_API.Data {
    public class Permit {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey("Weapon")]
        public List<Guid> Weapons { get; set; }

        [Required]
        [ForeignKey("Shooter")]
        public Guid ShooterId { get; set; }

        [Required]
        public DateTime PermitStart { get; set; }

        public DateTime PermitEnd { get; set; }

        [Required]
        [ForeignKey("Director")]
        public Guid DirectorId { get; set; }

        [Required]
        public string ShooterCPR { get; set; }

        [Required]
        public Association Association { get; set; }

        [Required]
        public string Purpose { get; set; }

    }

    public enum Association {
        [Description("Taarup - Kvols - Løgstrup Skytteforening")]
        TKL,

        [Description("Viborg Skytteforening")]
        Viborg
    }
}

using Microsoft.EntityFrameworkCore;

namespace RFID_tracking_API.Data {
    public class RFIDTrackingDbContext : DbContext {
        public RFIDTrackingDbContext(DbContextOptions options) : base(options) {
        }

        public DbSet<Shooter> Shooters { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Police> Police { get; set; }
        public DbSet<RegularUsers> RegularUsers { get; set; }
        //public DbSet<Permit> Permits { get; set; }
        public DbSet<Loan> Loans { get; set; }

    }
}

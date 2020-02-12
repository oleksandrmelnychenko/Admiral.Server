using Admiral.Server.Common;
using Admiral.Server.Databases.TableMaps;
using Admiral.Server.Databases.TableMaps.Identity;
using Admiral.Server.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace Admiral.Server.Databases {
    public class AdmiralDbContext : DbContext {

        public DbSet<UserIdentity> UserIdentities { get; set; }

        public AdmiralDbContext() { }

        public AdmiralDbContext(DbContextOptions<AdmiralDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(ConfigurationManager.DatabaseConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.AddConfiguration(new UserIdentityMap());
        }
    }
}

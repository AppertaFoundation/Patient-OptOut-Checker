using PatientOptOutAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace PatientOptOutAPI.Data
{
    public class LogContext : DbContext
    {
        public LogContext(DbContextOptions<LogContext> options) : base(options) { }

        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("PatientOptOut");
            modelBuilder.Entity<Log>()
                .Property(b => b.Username)
                .IsRequired();
        }
    }
}

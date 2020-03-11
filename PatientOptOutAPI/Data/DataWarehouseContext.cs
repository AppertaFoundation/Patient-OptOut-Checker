using PatientOptOutAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace PatientOptOutAPI.Data
{
    public class DataWarehouseContext : DbContext
    {
        public DataWarehouseContext(DbContextOptions<DataWarehouseContext> options) : base(options) { }

        //Sql View, maps the values to the table
        public DbSet<Numbers> vw_PatientOptOut { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Patient"); //See Database diagram, table schema is Patient, [Database].[Patient].[vw_PatientOptOut]
            base.OnModelCreating(modelBuilder);
        }
    }
}
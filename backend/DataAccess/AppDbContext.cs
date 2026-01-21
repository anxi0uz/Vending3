using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt){ }

        public DbSet<User> Users { get; set; }
        public DbSet<TradeApparatus> TradeApparatuses { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<NotificationLog> NotificationLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var ta = modelBuilder.Entity<TradeApparatus>();
            ta.HasIndex(x => x.SerialNumber).IsUnique();

            ta.ToTable(tb => tb.HasCheckConstraint(
                "CK_Resource_Positive",
                "\"Resource\" > 0"));

            ta.ToTable(tb => tb.HasCheckConstraint(
                "CK_RepairTime_Range",
                "\"RepairTime\" BETWEEN 1 AND 20"));

            ta.ToTable(tb => tb.HasCheckConstraint(
                "CK_NextCheckInterval_NonNegative",
                "\"NextCheckInterval\" >= 0"));

            ta.ToTable(tb => tb.HasCheckConstraint(
                "CK_DateUpdated_Range",
                "\"DateUpdated\" IS NULL OR (\"DateUpdated\" >= \"DateCreated\" AND \"DateUpdated\" <= CURRENT_DATE)")); ta.ToTable(tb => tb.HasCheckConstraint(
                "CK_LastCheckDate_Range",
                "\"LastCheckDate\" IS NULL OR (\"LastCheckDate\" >= \"DateCreated\" AND \"LastCheckDate\" <= CURRENT_DATE)"));

            ta.ToTable(tb => tb.HasCheckConstraint(
                "CK_InventarizationTime_Range",
                "\"InventarizationTime\" IS NULL OR (\"InventarizationTime\" >= \"DateCreated\" AND \"InventarizationTime\" <= CURRENT_DATE)"));

            ta.ToTable(tb => tb.HasCheckConstraint(
                "CK_NextRepairDate_Future",
                "\"NextRepairDate\" IS NULL OR \"NextRepairDate\" > CURRENT_DATE"));

            ta.Property<DateOnly?>("NextCheckDate")
                .HasComputedColumnSql(
                    "CASE WHEN \"LastCheckDate\" IS NULL THEN NULL ELSE \"LastCheckDate\" + (\"NextCheckInterval\" * INTERVAL '1 month') END",
                    stored: true);

            modelBuilder.Entity<Sales>().ToTable(tb => tb.HasCheckConstraint(
                "CK_TotalPrice_NonNegative",
                "\"TotalPrice\" >= 0"));
        }
    }
}

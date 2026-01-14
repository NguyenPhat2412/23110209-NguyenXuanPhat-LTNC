using GarageManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageManagement.DAL
{
    public class GarageContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<RepairOrderDetail> RepairOrderDetails { get; set; }
        public DbSet<RepairOrder> RepairOrders { get; set; }
        public DbSet<UserAccount> UserAccounts{ get; set ; }

        public GarageContext()
        {

        }

        public GarageContext(DbContextOptions<GarageContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (!optionsBuilder.IsConfigured)
            {
                var cs = ConfigurationManager.ConnectionStrings["GarageDb"]?.ConnectionString;
                if (string.IsNullOrWhiteSpace(cs))
                    throw new Exception("Chưa cấu hình connection string 'GarageDb' trong App.config");

                optionsBuilder.UseSqlServer(cs);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(e =>
            {
                e.ToTable("Customers");
                e.HasKey(x => x.CustomerId);

                e.Property(x => x.FullName).IsRequired().HasMaxLength(200);
                e.Property(x => x.Phone).IsRequired().HasMaxLength(20);
                e.Property(x => x.Address).IsRequired().HasMaxLength(300);

                e.HasMany(x => x.Cars)
                 .WithOne(x => x.Customer)
                 .HasForeignKey(x => x.CustomerId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Car>(e =>
            {
                e.ToTable("Cars");
                e.HasKey(x => x.CarId);

                e.Property(x => x.LicensePlate).IsRequired().HasMaxLength(20);
                e.Property(x => x.Brand).IsRequired().HasMaxLength(100);
                e.Property(x => x.Model).IsRequired().HasMaxLength(100);

                e.HasIndex(x => x.LicensePlate).IsUnique();

                e.HasMany(x => x.RepairOrders)
                 .WithOne(x => x.Car)
                 .HasForeignKey(x => x.CarId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Part>(e =>
            {
                e.ToTable("Parts");
                e.HasKey(x => x.PartId);

                e.Property(x => x.PartName).IsRequired().HasMaxLength(200);
                e.Property(x => x.UnitPrice).HasPrecision(18, 2);
            });

            modelBuilder.Entity<RepairOrder>(e =>
            {
                e.ToTable("RepairOrders");
                e.HasKey(x => x.RepairOrderId);

                e.Property(x => x.Description).IsRequired().HasMaxLength(500);
                e.Property(x => x.TotalAmount).HasPrecision(18, 2);
                e.Property(x => x.CreatedDate).HasDefaultValueSql("GETDATE()");

                e.HasMany(x => x.Details)
                 .WithOne(x => x.RepairOrder)
                 .HasForeignKey(x => x.RepairOrderId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<RepairOrderDetail>(e =>
            {
                e.ToTable("RepairOrderDetails");
                e.HasKey(x => x.RepairOrderDetailId);

                e.Property(x => x.LaborCost).HasPrecision(18, 2);
                e.Property(x => x.LineTotal).HasPrecision(18, 2);

                e.HasOne(x => x.Part)
                 .WithMany()
                 .HasForeignKey(x => x.PartId)
                 .OnDelete(DeleteBehavior.Restrict);

                e.HasIndex(x => new { x.RepairOrderId, x.PartId });
            });

            modelBuilder.Entity<UserAccount>(e =>
            {
                e.ToTable("UserAccounts");
                e.HasKey(x => x.UserAccountId);

                e.Property(x => x.Username).IsRequired().HasMaxLength(50);
                e.Property(x => x.PasswordHash).IsRequired().HasMaxLength(200);
                e.Property(x => x.Role).IsRequired().HasMaxLength(30);

                e.HasIndex(x => x.Username).IsUnique();
            });
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var badAddedPart = ChangeTracker.Entries<GarageManagement.Domain.Entities.Part>()
                .Where(e => e.State == EntityState.Added)
                .Select(e => e.Entity)
                .FirstOrDefault(p => string.IsNullOrWhiteSpace(p.PartName));

            if (badAddedPart != null)
                throw new System.Exception($"Đang cố INSERT Part mới với PartName NULL. PartId hiện tại = {badAddedPart.PartId}");

            var badModifiedPart = ChangeTracker.Entries<GarageManagement.Domain.Entities.Part>()
                .Where(e => e.State == EntityState.Modified)
                .Select(e => e.Entity)
                .FirstOrDefault(p => string.IsNullOrWhiteSpace(p.PartName));

            if (badModifiedPart != null)
                throw new System.Exception($"Đang cố UPDATE PartId={badModifiedPart.PartId} với PartName NULL.");

            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}

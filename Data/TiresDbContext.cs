using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TireDrift.Models;
namespace TireDrift.Data
{
    public class TiresDbContext : IdentityDbContext
    {
        public TiresDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Tire> Tires { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<OrderTires> OrderTires { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuring the many-to-many relationship using the Enrollment table
            modelBuilder.Entity<OrderTires>()
                .HasKey(e => new { e.OrderId, e.TireId }); // Composite primary key

            modelBuilder.Entity<OrderTires>()
                .HasOne(e => e.Order)
                .WithMany(s => s.Tires)
                .HasForeignKey(e => e.OrderId);

            modelBuilder.Entity<OrderTires>()
                .HasOne(e => e.Tire)
                .WithMany(c => c.Orders)
                .HasForeignKey(e => e.TireId);
        }
    }
}
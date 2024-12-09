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
            public DbSet<Invoice> Invoices { get; set; }
            public DbSet<Order> Orders { get; set; }
            public DbSet<Supplier> Suppliers { get; set; }
            public DbSet<Tire> Tires { get; set; }
            public DbSet<User> Users { get; set; }
    }
}
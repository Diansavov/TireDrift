using Microsoft.AspNetCore.Identity;
using TireDrift.Models;
namespace TireDrift.Data.Seeds
{
    public class SeedSuppliers
    {
        public static async Task Seed(IApplicationBuilder applicationBuilder)
        {
            using (var scope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var _context = scope.ServiceProvider.GetService<TiresDbContext>();
                var _userManager = scope.ServiceProvider.GetService<UserManager<User>>();

                if (!_context.Suppliers.Any())
                {
                    await _context.Suppliers.AddRangeAsync(
                    new Supplier()
                    {
                        Name = "Vankata",
                        PhoneNumber = "0887368909"
                    },
                    new Supplier()
                    {
                        Name = "Penkata",
                        PhoneNumber = "0887368909"
                    });

                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
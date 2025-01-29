
using Microsoft.AspNetCore.Identity;

namespace TestIgnatov.Data.Seeds
{
    class RolesSeed
    {
        public static async void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var scope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                await CreateRoles(roleManager);
            }
        }
        public static async Task CreateRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Manager"))
            {
                await roleManager.CreateAsync(new IdentityRole("Manager"));
                await roleManager.CreateAsync(new IdentityRole("Consultant"));
                await roleManager.CreateAsync(new IdentityRole("Technician"));
                await roleManager.CreateAsync(new IdentityRole("Logistics"));
            }
        }
    }
}
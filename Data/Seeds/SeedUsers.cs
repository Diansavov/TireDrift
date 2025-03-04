using Microsoft.AspNetCore.Identity;
using TireDrift.Models;
namespace TireDrift.Data.Seeds
{
    public class SeedUsers
    {
        public static async Task Seed(IApplicationBuilder applicationBuilder)
        {
            using (var scope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var _context = scope.ServiceProvider.GetService<TiresDbContext>();
                var _userManager = scope.ServiceProvider.GetService<UserManager<User>>();

                if (!_context.Users.Any())
                {
                    User manager = new User()
                    {
                        UserName = "Manager",
                        Email = "manager@manager.com",
                        FirstName = "Manager",
                        LastName = "Manager",
                        PhoneNumber = "08869420"
                    };
                    await _userManager.CreateAsync(manager, "123%Ab");
                    await _userManager.AddToRoleAsync(manager, "Manager");
                    User consultant = new User()
                    {
                        UserName = "Pencho",
                        Email = "pencho@gmail.com",
                        FirstName = "Pencho",
                        LastName = "Penevski",
                        PhoneNumber = "0885321426"
                    };
                    User consultant1 = new User()
                    {
                        UserName = "Pavlov",
                        Email = "pafkata@gmail.com",
                        FirstName = "Pavlov",
                        LastName = "PupnovaVruv",
                        PhoneNumber = "0885321426"
                    };
                    await _userManager.CreateAsync(consultant, "123%Ab");
                    await _userManager.AddToRoleAsync(consultant, "Consultant");
                    await _userManager.CreateAsync(consultant1, "123%Ab");
                    await _userManager.AddToRoleAsync(consultant1, "Consultant");
                    User technician = new User()
                    {
                        UserName = "Gosho",
                        Email = "gosho@gmail.com",
                        FirstName = "Gosho",
                        LastName = "Goshev",
                        PhoneNumber = "0885321426"
                    };
                    User technician1 = new User()
                    {
                        UserName = "Minka",
                        Email = "minka@gmail.com",
                        FirstName = "Minka",
                        LastName = "Minkova",
                        PhoneNumber = "0885321426"
                    };
                    await _userManager.CreateAsync(technician, "123%Ab");
                    await _userManager.AddToRoleAsync(technician, "Technician");
                    await _userManager.CreateAsync(technician1, "123%Ab");
                    await _userManager.AddToRoleAsync(technician1, "Technician");
                    User logisticsMan = new User()
                    {
                        UserName = "Izmeralda",
                        Email = "izi@gmail.com",
                        FirstName = "Izmeralda",
                        LastName = "Temperaturaizmervovala",
                        PhoneNumber = "0885321426"
                    };
                    User logisticsMan1 = new User()
                    {
                        UserName = "Johnny",
                        Email = "johnny@gmail.com",
                        FirstName = "Johnny",
                        LastName = "Sins",
                        PhoneNumber = "0885321426"
                    };
                    await _userManager.CreateAsync(logisticsMan, "123%Ab");
                    await _userManager.AddToRoleAsync(logisticsMan, "Logistics");
                     await _userManager.CreateAsync(logisticsMan1, "123%Ab");
                    await _userManager.AddToRoleAsync(logisticsMan1, "Logistics");
                }
            }
        }
    }
}
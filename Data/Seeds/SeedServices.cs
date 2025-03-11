using TireDrift.Models;
namespace TireDrift.Data.Seeds
{
    public class SeedServices
    {
        public static async Task Seed(IApplicationBuilder applicationBuilder)
        {
            using (var scope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var _context = scope.ServiceProvider.GetService<TiresDbContext>();
                await CreateServices(_context);
            }
        }
        public static async Task CreateServices(TiresDbContext _context)
        {
            if (!_context.Services.Any())
            {
                await _context.Services.AddRangeAsync(
                    new Service() {Name = "Смяна на гуми", Description = "Смяна на гумите на клиент с подходящи нови гуми", Price = 50, ImagePath = "/images/services/changeTires.png"},
                    new Service() {Name = "Баланс и монтаж", Description = "Баланс и монтаж на нови гуми", Price = 35, ImagePath = "/images/services/moutingTires.png"},
                    new Service() {Name = "Диагностика", Description = "Диагностика на гуми (налягане, износване и др.)", Price = 20, ImagePath = "/images/services/diagnosisTires.png"},
                    new Service() {Name = "Хотел за гуми", Description = "Складиране на гуми, през несезонният период", Price = 45, ImagePath = "/images/services/hotelTires.png"}

                );
                await _context.SaveChangesAsync();
            }
        }
    }
}
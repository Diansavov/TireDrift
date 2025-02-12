
using TireDrift.Data;
using TireDrift.Models;

namespace Services
{
    public class OrdersService : IOrdersService
    {
        private readonly TiresDbContext _tiresDbContext;
        public OrdersService(TiresDbContext tiresDbContext)
        {
            _tiresDbContext = tiresDbContext;
        }
        public async Task<Service> GetServiceAsync(string serviceId)
        {
            return await _tiresDbContext.Services.FindAsync(serviceId);
        }

        public async Task<Tire> GetTireAsync(string tireId)
        {
            return await _tiresDbContext.Tires.FindAsync(tireId);
        }
    }
}
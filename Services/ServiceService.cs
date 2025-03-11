using Microsoft.EntityFrameworkCore;
using TireDrift.Data;
using TireDrift.Models;

namespace Services
{
    public class ServiceService : IServiceService
    {
        private readonly TiresDbContext _tiresDbContext;
        public ServiceService(TiresDbContext tiresDbContext)
        {
            _tiresDbContext = tiresDbContext;
        }
        public List<Service> GetAll()
        {
            return _tiresDbContext.Services.ToList();
        }

        public async Task<Service> GetAsync(string id)
        {
            return await _tiresDbContext.Services.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
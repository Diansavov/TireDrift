using Microsoft.EntityFrameworkCore;
using TireDrift.Data;
using TireDrift.Models;
using TireDrift.Models.ViewModels;

namespace Services
{
    public class ServiceService : IServiceService
    {
        private readonly TiresDbContext _tiresDbContext;
        public ServiceService(TiresDbContext tiresDbContext)
        {
            _tiresDbContext = tiresDbContext;
        }

        public async Task EditAsync(ServiceViewModel serviceViewModel)
        {
            Service service = await GetAsync(serviceViewModel.Id);
            service.Description = serviceViewModel.Description;
            service.Price = serviceViewModel.Price;

            _tiresDbContext.Update(service);
            await _tiresDbContext.SaveChangesAsync();
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
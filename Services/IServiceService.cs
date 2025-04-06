using TireDrift.Models;
using TireDrift.Models.ViewModels;

namespace Services
{
    public interface IServiceService
    {
        List<Service> GetAll();
        Task<Service> GetAsync(string id);
        Task EditAsync(ServiceViewModel serviceViewModel);
    }
}
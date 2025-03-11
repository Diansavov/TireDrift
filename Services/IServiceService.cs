using TireDrift.Models;

namespace Services
{
    public interface IServiceService
    {
        List<Service> GetAll();
        Task<Service> GetAsync(string id);
    }
}
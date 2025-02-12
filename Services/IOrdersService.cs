using TireDrift.Models;

namespace Services
{
    public interface IOrdersService
    {
       Task<Tire> GetTireAsync(string tireId);
        Task<Service> GetServiceAsync(string serviceId);
    }
}
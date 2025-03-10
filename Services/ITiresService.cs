using TireDrift.Models;
using TireDrift.Models.ViewModels;

namespace Services
{
    public interface ITiresService
    {
        Task AddTireAsync(TireViewModel addTireViewModel);
        List<Tire> GetAll();
        Task<Tire> GetAsync(string id);
        Task EditAsync(TireViewModel editTireViewModel);
        Task DeleteAsync(string id);
        List<Tire> SearchProducts(string name, string filter);
        List<HotelTires> GetUserHotelTires(string userId);
        Task AddUserHotelTire(TireViewModel tireViewModel, string userId);
        Task RemoveUserHotelTire(string hotelTireId);
    }
}
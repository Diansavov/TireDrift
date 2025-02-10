using TireDrift.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using TireDrift.Models;

namespace TireDrift.Services
{
    public interface ISupplierService
    {
        Task AddSupplierAsync(SupplierViewModel addSupplier);

        Task EditAsync(SupplierViewModel editSupplier);
        Task DeleteAsync(string id);
        Task<Supplier> GetAsync(string id);
        List<Supplier> GetAll();

    }
}
using TireDrift.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using TireDrift.Models;
using TireDrift.Data;
using Microsoft.IdentityModel.Tokens;

namespace TireDrift.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly TiresDbContext _tiresDbContext;
        public SupplierService(TiresDbContext tiresDbContext)
        {
            _tiresDbContext = tiresDbContext;
        }
        public async Task AddSupplierAsync(SupplierViewModel addSupplier)
        {
            Supplier supplier = new Supplier()
            {
                Name = addSupplier.Name,
                PhoneNumber = addSupplier.PhoneNumber,
            };

            await _tiresDbContext.Suppliers.AddAsync(supplier);
            await _tiresDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            Supplier supplier = await GetAsync(id);

            _tiresDbContext.Suppliers.Remove(supplier);
            await _tiresDbContext.SaveChangesAsync();
        }

        public async Task EditAsync(SupplierViewModel editSupplier)
        {
            Supplier tire = await GetAsync(editSupplier.Id);
            tire.Name = editSupplier.Name;
            tire.PhoneNumber = editSupplier.PhoneNumber;

            _tiresDbContext.Update(tire);
            await _tiresDbContext.SaveChangesAsync();
        }

        public List<Supplier> GetAll()
        {
            return _tiresDbContext.Suppliers.ToList();

        }

        public async Task<Supplier> GetAsync(string id)
        {
            return await _tiresDbContext.Suppliers.FindAsync(id);
        }

        public List<Supplier> GetSearched(string name)
        {
            List<Supplier> suppliers = GetAll();

            if (!name.IsNullOrEmpty())
            {
                suppliers = suppliers.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToList();
            }
            return suppliers;
        }
    }
}
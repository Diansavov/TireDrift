
using Microsoft.EntityFrameworkCore;
using TireDrift.Data;
using TireDrift.Models;
using TireDrift.Services;

namespace Services
{
    public class OrdersService : IOrdersService
    {
        private readonly TiresDbContext _tiresDbContext;
        private readonly ITiresService _tiresService;
        private readonly ISupplierService _supplierService;
        public OrdersService(TiresDbContext tiresDbContext, ISupplierService supplierService, ITiresService tiresService)
        {
            _tiresDbContext = tiresDbContext;
            _tiresService = tiresService;
            _supplierService = supplierService;
        }

        public async Task FinishOrder(Cart cart, string clientId)
        {
            Random random = new Random();
            List<Supplier> suppliers = _supplierService.GetAll();
            Order order = new Order()
            {
                ClientId = clientId,
                Date = DateTime.Now,
                Supplier = suppliers[random.Next(0, suppliers.Count)],
                Tires = cart.Tires,
                Services = cart.Services,
                TotalPrice = cart.TotalPrice
            };
            foreach (var tire in cart.Tires)
            {
                Tire stockTire = await _tiresService.GetAsync(tire.Id);
                stockTire.Stock -= tire.Quantity;

                if (stockTire.Stock - tire.Quantity < 0)
                {
                    //Invalid Stock (not enough of the item)
                    cart.Tires.Remove(tire);
                }
                else
                {
                    stockTire.Stock -= tire.Quantity;
                    _tiresDbContext.Tires.Update(stockTire);
                    await _tiresDbContext.SaveChangesAsync();
                }
            }
            foreach (var service in cart.Services)
            {
                _tiresDbContext.Entry(service).State = EntityState.Unchanged;
            }

            await _tiresDbContext.Orders.AddAsync(order);
            await _tiresDbContext.SaveChangesAsync();
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
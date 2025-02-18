
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
            if (cart.Tires.IsNullOrEmpty() && cart.Services.IsNullOrEmpty())
            {
                return;
            }
            Random random = new Random();
            List<Supplier> suppliers = _supplierService.GetAll();
            Order order = new Order()
            {
                ClientId = clientId,
                Date = DateTime.Now,
                Supplier = suppliers[random.Next(0, suppliers.Count)],
                TotalPrice = cart.TotalPrice,
                Tires = new List<OrderTires>(),
                Services = new List<Service>()
            };
            foreach (var tire in cart.Tires)
            {
                Tire stockTire = await _tiresService.GetAsync(tire.Id);
                stockTire.Stock -= tire.Quantity;

                if (stockTire.Stock - tire.Quantity < 0)
                {
                    //Invalid Stock (not enough of the item)
                    order.TotalPrice -= stockTire.Price * stockTire.Quantity;
                }
                else
                {
                    stockTire.Stock -= tire.Quantity;
                    stockTire.Orders = new List<OrderTires> { order };
                    order.Tires.Add(stockTire);
                    _tiresDbContext.Tires.Update(stockTire);
                }
            }
            foreach (var service in cart.Services)
            {
                _tiresDbContext.Entry(service).State = EntityState.Unchanged;
                order.Services.Add(service);
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
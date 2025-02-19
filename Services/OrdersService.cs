
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
                TotalPrice = 0,
                Tires = new List<OrderTires>(),
                Services = new List<Service>()
            };

            await _tiresDbContext.Orders.AddAsync(order);

            foreach (Tire tire in cart.Tires)
            {
                Tire stockTire = await _tiresService.GetAsync(tire.Id);
                OrderTires orderTires = new OrderTires()
                {
                    OrderId = order.Id,
                    TireId = tire.Id
                };

                stockTire.Stock -= tire.Quantity;

                if (stockTire.Stock - tire.Quantity < 0)
                {
                    //Invalid Stock (not enough of the item)
                    cart.TotalPrice -= stockTire.Price * stockTire.Quantity;
                }
                else
                {
                    orderTires.TireQuanity = tire.Quantity;
                    stockTire.Stock -= tire.Quantity;

                    _tiresDbContext.Tires.Update(stockTire);
                    _tiresDbContext.Entry(stockTire).State = EntityState.Modified;
                    await _tiresDbContext.OrderTires.AddAsync(orderTires);

                }
            }
            foreach (var service in cart.Services)
            {
                _tiresDbContext.Entry(service).State = EntityState.Unchanged;
                order.Services.Add(service);
            }

            order.TotalPrice = cart.TotalPrice;

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
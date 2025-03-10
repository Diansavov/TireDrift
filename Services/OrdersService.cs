
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TireDrift.Data;
using TireDrift.Models;
using TireDrift.Models.ViewModels;
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

        public async Task FinishInvoice(InvoiceViewModel invoiceViewModel, string orderId, string clientId)
        {
            Invoice invoice = new Invoice()
            {
                BulStat = invoiceViewModel.BulStat,
                CompanyName = invoiceViewModel.CompanyName,
                ClientId = clientId,
                Date = DateTime.Now,
                OrderId = orderId,
            };
            await _tiresDbContext.Invoices.AddAsync(invoice);
            await _tiresDbContext.SaveChangesAsync();
        }

        public async Task<string> FinishOrder(Cart cart, string clientId)
        {
            Random random = new Random();
            List<Supplier> suppliers = _supplierService.GetAll();
            Order order = new Order()
            {
                ClientId = clientId,
                Date = DateTime.Now,
                Supplier = suppliers[random.Next(0, suppliers.Count)],
                TotalPrice = cart.TotalPrice,
                Tires = new List<OrderTires>(),
                Services = new List<Service>(),
            };

            await _tiresDbContext.Orders.AddAsync(order);

            foreach (Tire tire in cart.Tires)
            {
                OrderTires orderTires = new OrderTires()
                {
                    OrderId = order.Id,
                    TireId = tire.Id
                };

                await _tiresDbContext.OrderTires.AddAsync(orderTires);
            }
            foreach (var service in cart.Services)
            {
                _tiresDbContext.Entry(service).State = EntityState.Unchanged;
                order.Services.Add(service);
            }

            await _tiresDbContext.SaveChangesAsync();
            return order.Id;
        }

        public async Task<Service> GetServiceAsync(string serviceId)
        {
            return await _tiresDbContext.Services.FindAsync(serviceId);
        }

        public async Task<Tire> GetTireAsync(string tireId)
        {
            return await _tiresDbContext.Tires.FindAsync(tireId);
        }

        public List<UserOrderDto> GetUserOrder(string userId)
        {
            return _tiresDbContext.Orders
                .Where(x => x.ClientId == userId)
                .Include(x => x.Client)
                .Include(x => x.Services)
                .Include(x => x.Supplier)
                .Include(x => x.Tires)
                .Select(order => new UserOrderDto
                {
                    Id = order.Id,
                    TotalPrice = order.TotalPrice,
                    OrderDate = order.Date,
                    SupplierUserName = order.Supplier.Name,
                    SupplierPhoneNum = order.Supplier.PhoneNumber,
                    OrderedServices = order.Services.Select(x => x.Name).ToList(),
                    OrderedTires = order.Tires.Select(x => x.Tire).Select(x => x.Name).ToList(),
                })
                .ToList();
        }
    }
}
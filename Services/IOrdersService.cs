using TireDrift.Models;
using TireDrift.Models.ViewModels;

namespace Services
{
    public interface IOrdersService
    {
       Task<Tire> GetTireAsync(string tireId);
        Task<Service> GetServiceAsync(string serviceId);
        Task<string> FinishOrder(Cart cart, string clientId);
        Task FinishInvoice(InvoiceViewModel invoiceViewModel, string orderId, string clientId);
    }
}
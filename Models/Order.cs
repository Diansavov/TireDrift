using System.ComponentModel.DataAnnotations.Schema;

namespace TireDrift.Models
{
    public class Order
    {
        public Order()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        [ForeignKey("Client")]
        public string ClientId { get; set; }
        public User Client { get; set; }
        [ForeignKey("Supplier")]
        public string SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public DateTime Date { get; set; }
        public List<OrderTires> Tires { get; set; }
        public List<Service> Services { get; set; }
        public decimal TotalPrice { get; set; }
    
    }
}
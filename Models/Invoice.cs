using System.ComponentModel.DataAnnotations.Schema;

namespace TireDrift.Models
{
    public class Invoice
    {
        public Invoice()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string BulStat { get; set; }
        public string CompanyName { get; set; }
        [ForeignKey("Client")]
        public string ClientId { get; set; }
        public User Client { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("Order")]
        public string OrderId { get; set; }
        public Order Order { get; set; }
    }
}
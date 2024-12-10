using Microsoft.AspNetCore.SignalR.Protocol;

namespace TireDrift.Models
{
    public class Service
    {
        public Service()
        {
            Id = Guid.NewGuid().ToString();
        }
        
        public string Id { get; set;}
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public List<Order> Orders { get; set; }
    }
}
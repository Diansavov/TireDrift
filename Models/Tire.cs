using Microsoft.AspNetCore.SignalR.Protocol;

namespace TireDrift.Models
{
    public class Tire
    {
        public Tire()
        {
            Id = Guid.NewGuid().ToString();
        }
        
        public string Id { get; set;}
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
    }
}
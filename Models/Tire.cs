using Microsoft.AspNetCore.SignalR.Protocol;
using TireDrift.Models.ViewModels;

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
        public string ImagePath { get; set; }
        public List<Order> Orders { get; set; }
    }
}
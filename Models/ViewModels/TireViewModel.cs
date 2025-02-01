using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.SignalR.Protocol;

namespace TireDrift.Models.ViewModels
{
    public class TireViewModel
    {            
        public TireViewModel(Tire tire)
        {
            Id = tire.Id;
            Name = tire.Name;
            Price = Math.Round(tire.Price, 2);
            Stock = tire.Stock;
            Description = tire.Description;
            EditImagePath = tire.ImagePath;
        }
        public TireViewModel()
        {
            
        }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        public string? EditImagePath { get; set; }
        public string? Id { get; set; }
    }
}
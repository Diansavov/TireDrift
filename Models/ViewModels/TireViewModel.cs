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
        [Required(ErrorMessage = "Името на гумата е задължително")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Цената е задължителна")]
        [Range(0, double.MaxValue, ErrorMessage = "Цената трябва да е положителна")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Въведете число")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Количеството е задължително")]
        [Range(0, double.MaxValue, ErrorMessage = "Количеството трябва да е положително")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Въведете цяло число")]
        public int Stock { get; set; }
        [Required(ErrorMessage = "Описанието е задължително")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Изображението е задължително")]
        public IFormFile Image { get; set; }
        public string? EditImagePath { get; set; }
        public string? Id { get; set; }
    }
}
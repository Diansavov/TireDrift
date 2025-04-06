using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.SignalR.Protocol;

namespace TireDrift.Models.ViewModels
{
    public class ServiceViewModel
    {            
        public ServiceViewModel(Service service)
        {
            Id = service.Id;
            Name = service.Name;
            Price = Math.Round(service.Price, 2);
            Description = service.Description;
        }
        public ServiceViewModel()
        {
            
        }
        [Required(ErrorMessage = "Цената е задължителна")]
        [Range(0, double.MaxValue, ErrorMessage = "Цената трябва да е положителна")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Въведете число")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Описанието е задължително")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Изображението е задължително")]
        public IFormFile Image { get; set; }
        public string? Id { get; set; }
        public string? Name { get; set; }
    }
}
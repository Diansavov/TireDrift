using System.ComponentModel.DataAnnotations;

namespace TireDrift.Models.ViewModels
{
    class AddTireViewModel
    {            
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
    }
}
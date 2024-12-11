using System.ComponentModel.DataAnnotations;

namespace TireDrift.Models.ViewModels
{
    class LogInViewMode
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
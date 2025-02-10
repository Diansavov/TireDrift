using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace TireDrift.Models.ViewModels
{
    public class SupplierViewModel
    {
        public string? Id { get; set; }
        [Required(ErrorMessage = "Личното име е задължително")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Телефонният номер е задължителен")]
        public string PhoneNumber { get; set; }
    }
}
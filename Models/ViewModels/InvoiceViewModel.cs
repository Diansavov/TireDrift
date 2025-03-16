using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace TireDrift.Models.ViewModels
{
    public class InvoiceViewModel
    {
        [Required(ErrorMessage = "БулСтат е задължителен")]
        [StringLength(12, MinimumLength = 9,ErrorMessage = "БулСтат трябва да е от 9 до 12 дълъг")]
        [RegularExpression(@"^\d+$", ErrorMessage = "БулСтат трябва да съдържа само цифри")]
        public string BulStat { get; set; }
        [Required(ErrorMessage = "Името на компанията е задължително")]
        public string CompanyName { get; set; }
        public string? ClientFirstName { get; set; }
        public string? ClientLastName { get; set; }
    }
}
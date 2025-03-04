using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace TireDrift.Models.ViewModels
{
    public class InvoiceViewModel
    {
        [Required(ErrorMessage = "БулСтат име е задължително")]
        public string BulStat { get; set; }
        [Required(ErrorMessage = "Личното име е задължително")]
        public string CompanyName { get; set; }
        public string? ClientFirstName { get; set; }
        public string? ClientLastName { get; set; }
    }
}
using System.ComponentModel.DataAnnotations.Schema;

namespace TireDrift.Models
{
    public class UserInvoiceDto
    {
        public string Id { get; set; }
        public string BulStat { get; set; }
        public string CompanyName { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalPrice { get; set; }
        public List<string> OrderedTires { get; set; }
        public List<string> OrderedServices { get; set; }

    }
}
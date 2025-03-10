using System.ComponentModel.DataAnnotations.Schema;

namespace TireDrift.Models
{
    public class UserOrderDto
    {
        public string Id { get; set; }
        public string SupplierUserName { get; set; }
        public string SupplierPhoneNum { get; set; }
        public DateTime OrderDate { get; set; }
        public List<string> OrderedTires { get; set; }
        public List<string> OrderedServices { get; set; }
        public decimal TotalPrice { get; set; }

    }
}
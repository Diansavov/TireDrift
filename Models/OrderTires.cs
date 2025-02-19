using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TireDrift.Models;

public class OrderTires
{
    public OrderTires()
    {
        Id = Guid.NewGuid().ToString();
    }
    [Key]
    public string Id { get; set; }
    [ForeignKey("Order")]
    public string OrderId { get; set; }
    public Order Order { get; set; }
    [ForeignKey("Tire")]
    public string TireId { get; set; }
    public Tire Tire { get; set; }
    public int TireQuanity { get; set; }
}
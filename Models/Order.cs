namespace TireDrift.Models
{
    public class Order
    {
        public Order()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public User Client { get; set; }
        public Supplier Supplier { get; set; }
        public DateTime Date { get; set; }
        public List<Tire> Tires { get; set; }
        public List<Service> Services { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }

    }
}
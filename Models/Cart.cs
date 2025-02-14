namespace TireDrift.Models
{
    class Cart
    {
        public Cart()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public List<Tire> Tires { get; set; }
        public List<Service> Services { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
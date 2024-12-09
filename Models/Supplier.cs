namespace TireDrift.Models
{
    public class Supplier
    {
        public Supplier()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}
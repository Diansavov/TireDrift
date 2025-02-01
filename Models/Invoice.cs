namespace TireDrift.Models
{
    public class Invoice
    {
        public Invoice()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string BulStat { get; set; }
        public User Client { get; set; }
        public DateTime Date { get; set; }
        public Order Order { get; set; }
    }
}
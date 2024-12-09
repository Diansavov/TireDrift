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
        //Question?? Poruchka ili 3te poleta?
        public Order Order { get; set; }
    }
}
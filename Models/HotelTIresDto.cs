using System.ComponentModel.DataAnnotations.Schema;

namespace TireDrift.Models
{
    public class HotelTiresDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string TireName { get; set; }
        public string TireImageUrl { get; set; }
        public int TireQuanity { get; set; }
    }
}
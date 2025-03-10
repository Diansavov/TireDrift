using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TireDrift.Models;

namespace TireDrift.Models
{
    public class HotelTires
    {
        public HotelTires()
        {
            Id = Guid.NewGuid().ToString();
        }
        [Key]
        public string Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }
        public string TireName { get; set; }
        public string TireImageUrl { get; set; }
        public int TireQuanity { get; set; }
    }
}
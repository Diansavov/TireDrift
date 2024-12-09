using Microsoft.AspNetCore.Identity;

namespace TireDrift.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
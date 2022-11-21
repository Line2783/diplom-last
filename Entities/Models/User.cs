using Microsoft.AspNetCore.Identity;

namespace Entities.Models
{
    public class User : IdentityUser
    {
        public string INN { get; set; }
        public string HotelName { get; set; }
    }
}
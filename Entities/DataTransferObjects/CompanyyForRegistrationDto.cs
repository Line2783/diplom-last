using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class CompanyyForRegistrationDto
    {
        [Required(ErrorMessage = "INN is required")]
        public string INN { get; set; }
        [Required(ErrorMessage = "HotelName is required")]
        public string HotelName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public ICollection<string> Roles { get; set; }
    }
}
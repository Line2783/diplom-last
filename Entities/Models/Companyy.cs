using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Entities.Models
{
    public class Companyy : IdentityUser
    {
        [Required(ErrorMessage = "INN is required")]
        [MinLength(12, ErrorMessage = "Minimum length for the INN is 12 characters.")]
        [MaxLength(12, ErrorMessage = "Maximum length for the INN is 12 characters.")]
        public string INN { get; set; }
        [Required(ErrorMessage = "HotelName is required")]
        public string HotelName { get; set; }
    }
}
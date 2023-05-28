using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Entities.Models
{
    public class Companyy : IdentityUser
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "INN is required")]
        [MinLength(12, ErrorMessage = "Minimum length for the INN is 12 characters.")]
        [MaxLength(12, ErrorMessage = "Maximum length for the INN is 12 characters.")]
        public string INN { get; set; }
        [Required(ErrorMessage = "HotelName is required")]
        public string HotelName { get; set; }
        public ICollection<Advertisement> Advertisements { get; set; } // свойство навигации для объявлений
    }
}
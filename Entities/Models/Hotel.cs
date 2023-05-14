using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Hotel
    {
        [Column("HotelId")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "INN is required")]
        [MinLength(12, ErrorMessage = "Minimum length for the INN is 12 characters.")]
        [MaxLength(12, ErrorMessage = "Maximum length for the INN is 12 characters.")]
        public string INN { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "HotelName is required")]
        public string HotelName { get; set; }
        public ICollection<Advertisement> Advertisements { get; set; }
        public ICollection<HotelPhoto> Image { get; set; }

    }
}
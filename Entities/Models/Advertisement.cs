using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Entities.Models
{
    public class Advertisement
    {
        [Column("AdvertisementId")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Advertisement name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")] 
        public string Name { get; set; }
        [Required(ErrorMessage = "Advertisement city is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for rhe Address is 60 character")]
        public string City { get; set; }
        [Required(ErrorMessage = "Advertisement address is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for rhe Address is 60 character")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Advertisement description is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for rhe Address is 60 character")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Advertisement number is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for rhe Address is 60 character")]
        public string Number { get; set; }
        [Required(ErrorMessage = "Advertisement cat is a required field.")]
        public Boolean Cat { get; set; }
        [Required(ErrorMessage = "Advertisement dog is a required field.")]
        public Boolean Dog { get; set; }
        [Required(ErrorMessage = "Advertisement rodent is a required field.")]
        public Boolean Rodent { get; set; }
        [Required(ErrorMessage = "Advertisement other is a required field.")]
        public Boolean Other { get; set; }
        
        public ICollection<ProductPhoto> Image { get; set; }

        // public List<Animals> AnimalsList { get; set; }
        //public ICollection<ProductPhoto> Image { get; set; }

        
        public Guid HotelId { get; set; }
        public Hotel Hotel { get; set; }
        
    }

    // public enum Animals
    // {
    //     Cat,
    //     Dog, 
    //     Rodent, 
    //     Other, 
    // }
}
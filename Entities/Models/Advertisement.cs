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
            public Guid AdvertisementId { get; set; }

            [Required(ErrorMessage = "Advertisement name is a required field.")]
            [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
            public string Name { get; set; }

            [Required(ErrorMessage = "Advertisement city is a required field.")]
            [MaxLength(60, ErrorMessage = "Maximum length for the Address is 60 character")]
            public string City { get; set; }

            [Required(ErrorMessage = "Advertisement address is a required field.")]
            [MaxLength(60, ErrorMessage = "Maximum length for the Address is 60 character")]
            public string Address { get; set; }

            [Required(ErrorMessage = "Advertisement description is a required field.")]
            [MaxLength(1000, ErrorMessage = "Maximum length for the Address is 60 character")]
            public string Description { get; set; }

            [Required(ErrorMessage = "Advertisement number is a required field.")]
            public string Number { get; set; }

            [Required(ErrorMessage = "Advertisement cat is a required field.")]
            public bool Cat { get; set; }

            [Required(ErrorMessage = "Advertisement dog is a required field.")]
            public bool Dog { get; set; }

            [Required(ErrorMessage = "Advertisement rodent is a required field.")]
            public bool Rodent { get; set; }

            [Required(ErrorMessage = "Advertisement other is a required field.")]
            public bool Other { get; set; }
            
            public ICollection<ProductPhoto> Photos { get; set; }
            
            public string? CompanyId { get; set; }
            public virtual Companyy Companyy { get; set; }
    }
}

    

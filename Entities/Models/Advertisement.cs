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
            [MaxLength(150, ErrorMessage = "Maximum length for the Address is 60 character")]
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
            public virtual Companyy Companyy { get; set; } // изменено название свойства навигации
            
    }
    
        
        
        
        
        
       //  [Column("AdvertisementId")]
       //  public Guid Id { get; set; }
       //  [Required(ErrorMessage = "Advertisement name is a required field.")]
       //  [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")] 
       //  
       //  [ForeignKey(nameof(Companyy))]
       //  public Guid UserId { get; set; }
       //  public Companyy Companyy { get; set; }
       //  public string Name { get; set; }
       //  [Required(ErrorMessage = "Advertisement city is a required field.")]
       //  [MaxLength(60, ErrorMessage = "Maximum length for rhe Address is 60 character")]
       //  public string City { get; set; }
       //  [Required(ErrorMessage = "Advertisement address is a required field.")]
       //  [MaxLength(60, ErrorMessage = "Maximum length for rhe Address is 60 character")]
       //  public string Address { get; set; }
       //  [Required(ErrorMessage = "Advertisement description is a required field.")]
       //  [MaxLength(150, ErrorMessage = "Maximum length for rhe Address is 60 character")]
       //  public string Description { get; set; }
       //  [Required(ErrorMessage = "Advertisement number is a required field.")]
       // // [MaxLength(60, ErrorMessage = "Maximum length for rhe Address is 60 character")]
       //  public string Number { get; set; }
       //  [Required(ErrorMessage = "Advertisement cat is a required field.")]
       //  public Boolean Cat { get; set; }
       //  [Required(ErrorMessage = "Advertisement dog is a required field.")]
       //  public Boolean Dog { get; set; }
       //  [Required(ErrorMessage = "Advertisement rodent is a required field.")]
       //  public Boolean Rodent { get; set; }
       //  [Required(ErrorMessage = "Advertisement other is a required field.")]
       //  public Boolean Other { get; set; }
       //  
       //  public ICollection<ProductPhoto> Photos { get; set; }
        
        //public List<ProductPhoto> Image { get; set; }
        // public List<Animals> AnimalsList { get; set; }
        //public ICollection<ProductPhoto> Image { get; set; }

        // [ForeignKey(nameof(Hotel))]
        // public Guid HotelId { get; set; }
        // public Hotel Hotel { get; set; }
        
    }

    // public enum Animals
    // {
    //     Cat,
    //     Dog, 
    //     Rodent, 
    //     Other, 
    // }

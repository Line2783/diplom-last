using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Client
    {
        [Column("ClientId")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Client name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 50 characters.")] 
        public string Name { get; set; }
        [Required(ErrorMessage = "Client address is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for rhe Address is 60 character")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Age is a required field.")]
        public string Age { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
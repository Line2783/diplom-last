using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Order
    {
        [Column("OrderId")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Product name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Product is 30 characters.")] 
        public string Product { get; set; }
        [Required(ErrorMessage = "Quantity is a required field.")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Cost is a required field.")]
        [MaxLength(20, ErrorMessage = "Maximum length for the Cost is 20 characters.")]
        public int Cost { get; set; }
        [ForeignKey(nameof(Order))]
        public Guid ClientId { get; set; }
        public Client Client { get; set; }
    }
}
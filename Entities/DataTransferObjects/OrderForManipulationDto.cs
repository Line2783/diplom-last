using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public abstract class OrderForManipulationDto
    {
        [Required(ErrorMessage = "Order name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Order is 30 characters.")]
        public string Product { get; set; }
        [Range(3, int.MaxValue, ErrorMessage = "Quantity is required and it can't be lower than 3")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Cost is a required field.")]
        [MaxLength(20, ErrorMessage = "Maximum length for the Cost is 20 characters.")]
        public int Cost { get; set; }
    }
}
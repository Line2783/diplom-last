using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class ChangeEmailDto
    {
        
        
        [Required(ErrorMessage = "Email is required")]
        public string CurrentEmail { get; set; }

        [Compare("Email", ErrorMessage = "The Email and confirmation password do not match.")]
        public string NewEmail { get; set; }
        public string Password { get; set; }
    }
}
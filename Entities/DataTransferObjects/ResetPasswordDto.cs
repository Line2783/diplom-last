using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class ResetPasswordDto
    {
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string CurrentPassword { get; set; }

        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string NewPassword { get; set; }

    }
}
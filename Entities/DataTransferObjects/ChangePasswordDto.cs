namespace Entities.DataTransferObjects
{
    public class ChangePasswordDto
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }

    }
}
namespace AppParkingSys_Front.Models
{
    public class LoginViewModel
    {
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
    }
}

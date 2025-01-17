namespace AppParkingSys_Front.Models
{
    public class User
    {
        public required int Id { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public required string Role { get; set; }
        public User() { }
        public User(int id, string email, string pass, string role)
        { 
            Id = id;
            Email = email;
            PasswordHash = pass;
            Role = role;
        }
    }
}

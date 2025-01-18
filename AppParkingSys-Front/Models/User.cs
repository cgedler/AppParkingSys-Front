namespace AppParkingSys_Front.Models
{
    public class User
    {
        public  int? Id { get; set; }
        public  string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public string? Role { get; set; }
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

using AppParkingSys_Front.Models;

namespace AppParkingSys_Front.Interfaces.Services
{
    public interface IUserService
    {
        Task<List<User>?> GetUsersAsync(string token);
        Task<User?> GetUserById(int id, string token);
        Task<User> RegisterUser(User user, string token);
        Task<User> UpdateUser(int id, User user, string token);
        Task<User?> DeleteUser(int id, string token);
    }
}

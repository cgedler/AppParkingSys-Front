using AppParkingSys_Front.Models;

namespace AppParkingSys_Front.Interfaces.Services
{
    public interface IUserService
    {
        Task<List<User>?> GetUsersAsync(string token);
        Task<User?> GetUserById(int id);
        Task<User> RegisterUser(User user);
        Task<User> UpdateUser(int id, User user);
        Task<User> DeleteUser(int id);
    }
}

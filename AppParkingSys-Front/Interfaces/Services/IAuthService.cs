using AppParkingSys_Front.Models;

namespace AppParkingSys_Front.Interfaces.Services
{
    public interface IAuthService
    {
        Task<string> LoginAsync(string email, string password);
        Task<bool> IsTokenValidAsync();
    }
}

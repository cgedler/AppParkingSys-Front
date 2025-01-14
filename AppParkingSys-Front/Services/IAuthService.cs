using AppParkingSys_Front.Models;

namespace AppParkingSys_Front.Services
{
    public interface IAuthService
    {
        Task<string> LoginAsync(string email, string password);
        Task<bool> IsTokenValidAsync();
        Task<List<Ticket>?> GetTicketsAsync();
    }
}

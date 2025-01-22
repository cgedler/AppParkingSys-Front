using AppParkingSys_Front.Models;

namespace AppParkingSys_Front.Interfaces.Services
{
    public interface ITicketService
    {
        Task<Ticket?> GetTicketById(int id, string token);
        Task<List<Ticket>?> GetAll(string token);
        Task<List<Ticket>?> GetToPay(string token);
        Task<Ticket?> RegisterTicket(Ticket ticket);
    }
}

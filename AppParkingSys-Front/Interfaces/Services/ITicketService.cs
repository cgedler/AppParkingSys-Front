using AppParkingSys_Front.Models;

namespace AppParkingSys_Front.Interfaces.Services
{
    public interface ITicketService
    {
        Task<Ticket?> GetTicketById(int id);
        Task<IEnumerable<Ticket>?> GetAll();
        Task<Ticket> RegisterTicket(Ticket ticket);
        Task<Ticket> UpdateTicket(int id, Ticket ticket);
    }
}

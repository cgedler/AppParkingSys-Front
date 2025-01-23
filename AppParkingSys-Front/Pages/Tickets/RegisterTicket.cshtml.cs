using AppParkingSys_Front.Interfaces.Services;
using AppParkingSys_Front.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppParkingSys_Front.Pages.Tickets
{
    public class RegisterTicketModel : PageModel
    {
        private readonly ILogger<RegisterTicketModel> _logger;
        private readonly ITicketService _ticketService;
        public Ticket? ticket { get; set; }
        public RegisterTicketModel(ILogger<RegisterTicketModel> logger, ITicketService ticketService)
        {
            _logger = logger;
            _ticketService = ticketService;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            Ticket ticket = new Ticket();
            ticket.EntryTime = DateTime.Now;
            ticket.ExitTime = null;
            var result = await _ticketService.RegisterTicket(ticket);
            if (result != null)
            {
                var ticketId = result.Id;
                TempData["Message"] = ticketId + " Ticket was register";
            }
            else
            {
                TempData["ErrorMessage"] = "Could not save data.";
                _logger.LogError("Could not save data.");
                return RedirectToPage("/Error");
            }
            return Page();
        }
    }
}

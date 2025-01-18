using AppParkingSys_Front.Interfaces.Services;
using AppParkingSys_Front.Models;
using AppParkingSys_Front.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace AppParkingSys_Front.Pages.Tickets
{
    public class TicketGetAllModel : PageModel
    {
        private readonly ILogger<TicketGetAllModel> _logger;
        private readonly ITicketService _ticketService;
        public required List<Ticket> TicketList { get; set; }

        public TicketGetAllModel(ILogger<TicketGetAllModel> logger, ITicketService ticketService)
        {
            _logger = logger;
            _ticketService = ticketService;
        }
        public async Task<IActionResult> OnGet()
        {
            _logger.LogInformation("Inside Tickets");
            var result = await _ticketService.GetAll();

            TicketList = new List<Ticket>();
            if (result != null)
            {
                TicketList = (List<Ticket>)result;
            }

            //_logger.LogInformation("TicketController - get: Tickets " + data);
            return Page();

        }
    }
}

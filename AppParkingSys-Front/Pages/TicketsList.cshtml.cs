using AppParkingSys_Front.Models;
using AppParkingSys_Front.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace AppParkingSys_Front.Pages
{
    public class TicketsListModel : PageModel
    {
        private readonly ILogger<TicketsListModel> _logger;
        private readonly IAuthService _authService;
        public required List<Ticket> TicketList { get; set; }

        public TicketsListModel(ILogger<TicketsListModel> logger, IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }
        public async Task<IActionResult> OnGet()
        {
            _logger.LogInformation("Inside Tickets");
            var result = await _authService.GetTicketsAsync();

            TicketList = new List<Ticket>();
            if (result != null)
            {
                TicketList = result;
            }

            //_logger.LogInformation("TicketController - get: Tickets " + data);
            return Page();

        }
    }
}

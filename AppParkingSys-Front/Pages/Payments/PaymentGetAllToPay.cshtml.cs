using AppParkingSys_Front.Interfaces.Services;
using AppParkingSys_Front.Models;
using AppParkingSys_Front.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace AppParkingSys_Front.Pages.Payments
{
    public class PaymentGetAllToPayModel : PageModel
    {
        private readonly ILogger<PaymentGetAllToPayModel> _logger;
        private readonly ITicketService _ticketService;
        public List<Ticket>? TicketList { get; set; }
        public PaymentGetAllToPayModel(ILogger<PaymentGetAllToPayModel> logger, ITicketService ticketService)
        {
            _logger = logger;
            _ticketService = ticketService;
        }
        public async Task<IActionResult> OnGet()
        {
            var token = GetTokenFromCookie();
            if (token == null)
            {
                TempData["ErrorMessage"] = "No active token found for the request.";
                _logger.LogError("No active token found for the request.");
                return RedirectToPage("/Error");
            }
            var result = await _ticketService.GetToPay(token);
            TicketList = new List<Ticket>();
            if (result != null)
            {
                TicketList = (List<Ticket>)result;
            }
            else
            {
                TempData["ErrorMessage"] = "No information found to display.";
                _logger.LogError("No information found to display.");
                return RedirectToPage("/Error");
            }
            return Page();
        }
        public string? GetTokenFromCookie()
        {
            string? token = string.Empty;
            string? value = string.Empty;
            if (Request.Cookies.TryGetValue("AuthToken", out token))
            {
                var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>?>(token);
                if (dictionary != null)
                {
                    dictionary.TryGetValue("token", out value);
                }
                return value;
            }
            return null;
        }
    }
}

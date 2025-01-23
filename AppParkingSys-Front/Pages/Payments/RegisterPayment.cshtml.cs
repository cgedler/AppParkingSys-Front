using AppParkingSys_Front.Interfaces.Services;
using AppParkingSys_Front.Models;
using AppParkingSys_Front.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace AppParkingSys_Front.Pages.Payments
{
    public class RegisterPaymentModel : PageModel
    {
        private readonly ILogger<RegisterPaymentModel> _logger;
        private readonly ITicketService _ticketService;
        private readonly IPaymentService _paymentService;
        [BindProperty]
        public Ticket? ticket { get; set; }
        [BindProperty]
        public Payment? payment { get; set; }
        public RegisterPaymentModel(ILogger<RegisterPaymentModel> logger, ITicketService ticketService, IPaymentService paymentService)
        {
            _logger = logger;
            _ticketService = ticketService;
            _paymentService = paymentService;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            var token = GetTokenFromCookie();
            if (token == null)
            {
                TempData["ErrorMessage"] = "No active token found for the request.";
                _logger.LogError("No active token found for the request.");
                return RedirectToPage("/Error");
            }
            var result = await _ticketService.GetTicketById(id, token);
            if (result != null)
            {
                this.ticket = result;
            }
            else
            {
                TempData["ErrorMessage"] = "No information found to display.";
                _logger.LogError("No information found to display.");
                return RedirectToPage("/Error");
            }
            return Page();
        }
        public async Task<IActionResult> OnPost(int id)
        {
            var token = GetTokenFromCookie();
            if (token == null)
            {
                TempData["ErrorMessage"] = "No active token found for the request.";
                _logger.LogError("No active token found for the request.");
                return RedirectToPage("/Error");
            }
            if (this.ticket != null && this.payment != null)
            {
                this.payment.TicketId = id;
                this.payment.PaymentDate = DateTime.Now;
                var result_payment = await _paymentService.RegisterPayment(this.payment, token);
                this.ticket.ExitTime = DateTime.Now;
                var result_ticket = await _ticketService.UpdateTicket(id, this.ticket, token);
                if (result_ticket != null && result_payment != null)
                {
                    TempData["Message"] = "Ticket was update and the payment was recorded.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Could not save data.";
                    _logger.LogError("Could not save data.");
                    return RedirectToPage("/Error");
                }
            }
            return RedirectToPage("/Payments/PaymentGetAllToPay");
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

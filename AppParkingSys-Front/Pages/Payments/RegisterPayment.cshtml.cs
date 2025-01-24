using AppParkingSys_Front.Interfaces.Services;
using AppParkingSys_Front.Models;
using AppParkingSys_Front.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AppParkingSys_Front.Pages.Payments
{
    public class RegisterPaymentModel : PageModel
    {
        private readonly ILogger<RegisterPaymentModel> _logger;
        private readonly ITicketService _ticketService;
        private readonly IPaymentService _paymentService;
        private readonly ICalculateRateService _calculateRateService;
        [BindProperty]
        public Ticket? ticket { get; set; } = new Ticket();
        [BindProperty]
        public Payment? payment { get; set; } = new Payment();
        [BindProperty]
        public decimal? amountCalculate { get; set; }

        public RegisterPaymentModel(ILogger<RegisterPaymentModel> logger, ITicketService ticketService, IPaymentService paymentService, ICalculateRateService calculateRateService)
        {
            _logger = logger;
            _ticketService = ticketService;
            _paymentService = paymentService;
            _calculateRateService = calculateRateService;
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
            var result_ticket = await _ticketService.GetTicketById(id, token);
            if (result_ticket != null)
            {
                this.ticket = result_ticket;
                this.ticket.ExitTime = DateTime.Now;
                if (this.ticket.EntryTime != null && this.ticket.ExitTime != null)
                {
                    DateTime? entry = this.ticket.EntryTime;
                    DateTime? exit = this.ticket.ExitTime;
                    var result_amountcalculate = await _calculateRateService.CalculateRate(1, token, entry, exit);
                    if (result_amountcalculate != null)
                    {
                        this.amountCalculate = result_amountcalculate;
                    }
                }
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
                var result_ticket = await _ticketService.UpdateTicket(id, this.ticket, token);
                this.payment.TicketId = id;
                this.payment.PaymentDate = DateTime.Now;
                var result_payment = await _paymentService.RegisterPayment(this.payment, token);
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

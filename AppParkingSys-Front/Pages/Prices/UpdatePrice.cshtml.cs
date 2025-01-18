using AppParkingSys_Front.Interfaces.Services;
using AppParkingSys_Front.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace AppParkingSys_Front.Pages.Prices
{
    public class UpdatePriceModel : PageModel
    {
        private readonly ILogger<UpdatePriceModel> _logger;
        private readonly IPriceService _priceService;
        [BindProperty]
        public Price? price { get; set; }
        public UpdatePriceModel(ILogger<UpdatePriceModel> logger, IPriceService priceService)
        {
            _logger = logger;
            _priceService = priceService;
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
            var result = await _priceService.GetPriceById(id, token);
            if (result != null)
            {
                this.price = result;
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
            if (this.price != null)
            {
                var result = await _priceService.UpdatePrice(id, this.price, token);
                if (result != null)
                {
                    TempData["Message"] = "Price was update.";
                }
                else
                {
                    TempData["ErrorMessage"] = "No information found to display.";
                    _logger.LogError("No information found to display.");
                    return RedirectToPage("Error");
                }
            }
            return RedirectToPage("/Prices/PriceGetAll");
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

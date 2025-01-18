using AppParkingSys_Front.Interfaces.Services;
using AppParkingSys_Front.Models;
using AppParkingSys_Front.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace AppParkingSys_Front.Pages.Prices
{
    public class PriceGetAllModel : PageModel
    {
        private readonly ILogger<PriceGetAllModel> _logger;
        private readonly IPriceService _priceService;
        public List<Price>? PriceList { get; set; }
        public PriceGetAllModel(ILogger<PriceGetAllModel> logger, IPriceService priceService)
        {
            _logger = logger;
            _priceService = priceService;
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
            var result = await _priceService.GetPricesAsync(token);
            PriceList = new List<Price>();
            if (result != null)
            {
                PriceList = result;
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

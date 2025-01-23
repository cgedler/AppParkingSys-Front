using AppParkingSys_Front.Interfaces.Services;
using AppParkingSys_Front.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace AppParkingSys_Front.Pages.Prices
{
    public class RegisterPriceModel : PageModel
    {
        private readonly ILogger<RegisterPriceModel> _logger;
        private readonly IPriceService _priceService;
        [BindProperty]
        public Price? price { get; set; }
        public RegisterPriceModel(ILogger<RegisterPriceModel> logger, IPriceService priceService)
        {
            _logger = logger;
            _priceService = priceService;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
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
                var result = await _priceService.RegisterPrice(this.price, token);
                if (result != null)
                {
                    TempData["Message"] = "Price was register";
                }
                else
                {
                    TempData["ErrorMessage"] = "Could not save data.";
                    _logger.LogError("Could not save data.");
                    return RedirectToPage("/Error");
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

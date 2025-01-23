using AppParkingSys_Front.Interfaces.Services;
using AppParkingSys_Front.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace AppParkingSys_Front.Pages.Users
{
    public class RegisterUserModel : PageModel
    {
        private readonly ILogger<RegisterUserModel> _logger;
        private readonly IUserService _userService;
        [BindProperty]
        public User? user { get; set; }
        public RegisterUserModel(ILogger<RegisterUserModel> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
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
            if (this.user != null)
            {
                var result = await _userService.RegisterUser(this.user, token);
                if (result != null)
                {
                    TempData["Message"] = "User was register";
                }
                else
                {
                    TempData["ErrorMessage"] = "Could not save data.";
                    _logger.LogError("Could not save data.");
                    return RedirectToPage("/Error");
                }
            }
            return RedirectToPage("/Users/UsersGetAll");
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

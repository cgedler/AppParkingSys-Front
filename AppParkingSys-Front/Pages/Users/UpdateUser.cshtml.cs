using AppParkingSys_Front.Interfaces.Services;
using AppParkingSys_Front.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace AppParkingSys_Front.Pages.Users
{
    public class UpdateUserModel : PageModel
    {
        private readonly ILogger<UpdateUserModel> _logger;
        private readonly IUserService _userService;
        [BindProperty]
        public User? user { get; set; }
        public UpdateUserModel(ILogger<UpdateUserModel> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
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
            var result = await _userService.GetUserById(id, token);
            if (result != null)
            {
                this.user = result;
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
            if (this.user != null)
            {
                var result = await _userService.UpdateUser(id, this.user, token);
                if (result != null)
                {
                    TempData["Message"] = "User was update.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Could not save data.";
                    _logger.LogError("Could not save data.");
                    return RedirectToPage("Error");
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

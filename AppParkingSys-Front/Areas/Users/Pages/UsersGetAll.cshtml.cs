using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Reflection;
using AppParkingSys_Front.Interfaces.Services;
using AppParkingSys_Front.Models;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace AppParkingSys_Front.Areas.Users.Pages
{
    public class UsersGetAllModel : PageModel
    {
        private readonly ILogger<UsersGetAllModel> _logger;
        private readonly IUserService _userService;
        public required List<User> UserList { get; set; }

        public UsersGetAllModel(ILogger<UsersGetAllModel> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }
        public async Task<IActionResult> OnGet()
        {
            var token = GetTokenFromCookie();
            if (token == null)
            {
                TempData["ErrorMessage"] = "No active token found for the request.";
                _logger.LogError("No active token found for the request.");
                return RedirectToPage("Error");
            }
            var result = await _userService.GetUsersAsync(token);
            UserList = new List<User>();
            if (result != null)
            {
                UserList = result;
            }
            else
            {
                TempData["ErrorMessage"] = "No information found to display.";
                _logger.LogError("No information found to display.");
                return RedirectToPage("Error");
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

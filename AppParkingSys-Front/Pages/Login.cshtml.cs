using AppParkingSys_Front.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppParkingSys_Front.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ILogger<LoginModel> _logger;
        private readonly IAuthService _authService;

        public LoginModel(ILogger<LoginModel> logger, IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }
        [BindProperty]
        public required string Email { get; set; }
        [BindProperty]
        public required string Password { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            var token = await _authService.LoginAsync(Email, Password);
            if (!string.IsNullOrEmpty(token))
            {
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTime.UtcNow.AddHours(1)
                };
                HttpContext.Response.Cookies.Append("token", token, cookieOptions);
                _logger.LogInformation("Token: " + token);
                return RedirectToPage("/Privacy");
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return Page();
        }
        public void OnGet()
        {
        }
    }
}

using AppParkingSys_Front.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppParkingSys_Front.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IApiService _apiService;
        public AccountController(IApiService apiService, ILogger<AccountController> logger)
        {
            _logger = logger;
            _apiService = apiService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            _logger.LogInformation("AccountController - Login()");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _apiService.LoginAsync(email, password);
            if (user != null)
            {
                // Manejar inicio de sesión exitoso
                return RedirectToAction("Index", "Home");
            }

            // Manejar error de inicio de sesión
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View();
        }
    }
}
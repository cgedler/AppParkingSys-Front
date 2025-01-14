using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using AppParkingSys_Front.Models;
using Microsoft.AspNetCore.Authentication;

namespace AppParkingSys_Front.Pages;

public class PrivacyModel : PageModel
{
    private readonly ILogger<PrivacyModel> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public required string? Token { get; set; }

    public PrivacyModel(ILogger<PrivacyModel> logger, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }


    public async Task<IActionResult> OnGet()
    {
        //var accessToken = await HttpContext.GetTokenAsync("token");
        //var token = string.Empty;
        //var header = (string)HttpContext.Request.Headers.Authorization;
        //var message = HttpContext.Request.PathBase;

        //var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("token");

        _logger.LogInformation("Privacy message: " + accessToken);
        //_logger.LogInformation("Token Privacy: " + Token);

        return Page();
    }
}


using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using AppParkingSys_Front.Models;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;
using System.Net;
using System.Net.Http;

namespace AppParkingSys_Front.Pages;

public class PrivacyModel : PageModel
{
    private readonly ILogger<PrivacyModel> _logger;
    public PrivacyModel(ILogger<PrivacyModel> logger, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
    }
    public void OnGet()
    {
    }
}


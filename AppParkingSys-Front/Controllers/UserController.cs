using AppParkingSys_Front.Services;
using AppParkingSys_Front.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AppParkingSys_Front.Controllers
{
    public class UserController : Controller
    {
        private readonly IApiService _apiService;

        public UserController(IApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _apiService.GetUsersAsync();
            return View(data);
        }
    }
}

using AppParkingSys_Front.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace AppParkingSys_Front.Controllers
{
    public class TicketController : Controller
    {
        private readonly ILogger<TicketController> _logger;
        //private readonly ILogger<TicketController> _logger;
        private readonly IApiService _apiService;
        public TicketController( ILogger<TicketController> logger, IApiService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogDebug("Debug ****************************************************");
            _logger.LogInformation("TicketController - Get()");
            var data = await _apiService.GetTicketsAsync();
            _logger.LogInformation("TicketController - get: Tickets " + data);
            return View(data);
        }
    }
}

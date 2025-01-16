using AppParkingSys_Front.Interfaces.Services;
using AppParkingSys_Front.Models;
using Newtonsoft.Json;
using System.Net.Http;

namespace AppParkingSys_Front.Services
{
    public class TicketService : ITicketService
    {
        private readonly ILogger<TicketService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public TicketService(ILogger<TicketService> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        async Task<IEnumerable<Ticket>?> ITicketService.GetAll()
        {
            _logger.LogInformation("Inside IAuthService.GetTicketsAsync()");
            var client = _httpClientFactory.CreateClient("ApiClient");
            var response = await client.GetAsync("ticket");
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                _logger.LogInformation("Content JSON: " + content);
                return JsonConvert.DeserializeObject<List<Ticket>?>(content);
            }
            return null;
        }

        Task<Ticket?> ITicketService.GetTicketById(int id)
        {
            throw new NotImplementedException();
        }

        Task<Ticket> ITicketService.RegisterTicket(Ticket ticket)
        {
            throw new NotImplementedException();
        }

        Task<Ticket> ITicketService.UpdateTicket(int id, Ticket ticket)
        {
            throw new NotImplementedException();
        }

    }
}

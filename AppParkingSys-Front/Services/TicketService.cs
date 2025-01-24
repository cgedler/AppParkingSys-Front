using AppParkingSys_Front.Interfaces.Services;
using AppParkingSys_Front.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;

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
        async Task<List<Ticket>?> ITicketService.GetAll(string token)
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var httpResponse = await client.GetAsync("ticket");
            if (httpResponse.Content != null)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Ticket>?>(responseContent);
            }
            return null;
        }
        async Task<List<Ticket>?> ITicketService.GetToPay(string token)
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var httpResponse = await client.GetAsync("ticket/topay");
            if (httpResponse.Content != null)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Ticket>?>(responseContent);
            }
            return null;
        }
        async Task<Ticket?> ITicketService.GetTicketById(int id, string token)
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var httpResponse = await client.GetAsync("ticket/" + id);
            if (httpResponse.Content != null)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Ticket?>(responseContent);
            }
            return null;
        }
        async Task<Ticket?> ITicketService.RegisterTicket(Ticket ticket)
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            object obj = new
            {
                entryTime = ticket.EntryTime,
                exitTime = ticket.ExitTime
            };
            string dataToJson = JsonConvert.SerializeObject(obj);
            var httpContent = new StringContent(dataToJson, Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync("ticket", httpContent);
            if (httpResponse.Content != null)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Ticket?>(responseContent);
            }
            return null;
        }
        async Task<Ticket?> ITicketService.UpdateTicket(int id, Ticket ticket, string token)
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            object obj = new
            {
                id = id,
                entryTime = ticket.EntryTime,
                exitTime = ticket.ExitTime
            };
            string dataToJson = JsonConvert.SerializeObject(obj);
            var httpContent = new StringContent(dataToJson, Encoding.UTF8, "application/json");
            var httpResponse = await client.PutAsync("ticket/" + id, httpContent);
            if (httpResponse.Content != null)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Ticket?>(responseContent);
            }
            return null;
        }
    }
}

using AppParkingSys_Front.Models;
using AppParkingSys_Front.Pages;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace AppParkingSys_Front.Services
{
    public class AuthService : IAuthService
    {
        private readonly ILogger<AuthService> _logger;
        private readonly IHttpClientFactory _httpClientFactory; 
        private readonly IConfiguration _configuration;


        public AuthService(ILogger<AuthService> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
           
        }

        Task<bool> IAuthService.IsTokenValidAsync()
        {
            throw new NotImplementedException();
        }

        async Task<string> IAuthService.LoginAsync(string email, string password)
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            var loginData = new { email = email, password = password };
            string dataToJson = JsonConvert.SerializeObject(loginData);
            var httpContent = new StringContent(dataToJson, Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync("login", httpContent);
            if (httpResponse.Content != null)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                return responseContent;
            }
            return "null";
        }
        async Task<List<Ticket>?> IAuthService.GetTicketsAsync()
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
    }
}

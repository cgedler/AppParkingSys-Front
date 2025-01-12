using AppParkingSys_Front.Models;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;

namespace AppParkingSys_Front.Services
{
    public class ApiService : IApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<User?> LoginAsync(string email, string password)
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            var loginData = new { Email = email, Password = password };
            var response = await client.PostAsJsonAsync("user/login", loginData);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<User>();
        }
        public async Task<List<User>?> GetUsersAsync()
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            var response = await client.GetAsync("user");
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<User>?>(content);
            }
            return null;
        }
        public async Task<List<Ticket>?> GetTicketsAsync()
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            var response = await client.GetAsync("ticket");
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Ticket>?>(content);
            }
            return null;
        }

    }
}

using AppParkingSys_Front.Models;

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
    }
}

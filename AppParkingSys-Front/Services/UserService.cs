using AppParkingSys_Front.Interfaces.Services;
using AppParkingSys_Front.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace AppParkingSys_Front.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public UserService(ILogger<UserService> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }
        async Task<string?> IUserService.DeleteUser(int id, string token)
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var httpResponse = await client.DeleteAsync("user/" + id);
            if (httpResponse.Content != null)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<string?>(responseContent);
            }
            return null;
        }
        async Task<User?> IUserService.GetUserById(int id, string token)
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var httpResponse = await client.GetAsync("user/" + id);
            if (httpResponse.Content != null)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<User?>(responseContent);
            }
            return null;
        }
        async Task<List<User>?> IUserService.GetUsersAsync(string token)
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var httpResponse = await client.GetAsync("user");
            if (httpResponse.Content != null)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<User>?>(responseContent);
            }
            return null;
        }
        async Task<User?> IUserService.RegisterUser(User user, string token)
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            object obj = new
            {
                email = user.Email,
                passwordHash = user.PasswordHash,
                role = user.Role
            };
            string dataToJson = JsonConvert.SerializeObject(obj);
            var httpContent = new StringContent(dataToJson, Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync("user", httpContent);
            if (httpResponse.Content != null)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<User?>(responseContent);
            }
            return null;
        }
        async Task<User?> IUserService.UpdateUser(int id, User user, string token)
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            object obj = new { 
                id = id,
                email = user.Email,
                passwordHash = user.PasswordHash,
                role =  user.Role };
            string dataToJson = JsonConvert.SerializeObject(obj);
            var httpContent = new StringContent(dataToJson, Encoding.UTF8, "application/json");
            var httpResponse = await client.PutAsync("user/" + id, httpContent);
            if (httpResponse.Content != null)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<User?>(responseContent);
            }
            return null;
        }
    }
}

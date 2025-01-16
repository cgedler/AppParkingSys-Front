using AppParkingSys_Front.Interfaces.Services;
using AppParkingSys_Front.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

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

        Task<User> IUserService.DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        Task<User?> IUserService.GetUserById(int id)
        {
            throw new NotImplementedException();
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
        Task<User> IUserService.RegisterUser(User user)
        {
            throw new NotImplementedException();
        }

        Task<User> IUserService.UpdateUser(int id, User user)
        {
            throw new NotImplementedException();
        }
    }
}

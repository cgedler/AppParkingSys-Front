using AppParkingSys_Front.Interfaces.Services;
using AppParkingSys_Front.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;

namespace AppParkingSys_Front.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ILogger<PaymentService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public PaymentService(ILogger<PaymentService> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }
        async Task<Payment?> IPaymentService.RegisterPayment(Payment payment, string token)
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            object obj = new
            {
                Amount = payment.Amount,
                PaymentDate = DateTime.Now,
                ticketId = payment.TicketId 
            };
            string dataToJson = JsonConvert.SerializeObject(obj);
            _logger.LogError("obj " + dataToJson);
            var httpContent = new StringContent(dataToJson, Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync("payment", httpContent);
            if (httpResponse.Content != null)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Payment?>(responseContent);
            }
            return null;
        }
    }
}

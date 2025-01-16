using AppParkingSys_Front.Interfaces.Services;
using AppParkingSys_Front.Models;

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

        Task<Payment> IPaymentService.DeletePayment(int id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Payment>> IPaymentService.GetAll()
        {
            throw new NotImplementedException();
        }

        Task<Payment?> IPaymentService.GetPaymentById(int id)
        {
            throw new NotImplementedException();
        }

        Task<Payment> IPaymentService.RegisterPayment(Payment payment)
        {
            throw new NotImplementedException();
        }

        Task<Payment> IPaymentService.UpdatePayment(int id, Payment payment)
        {
            throw new NotImplementedException();
        }
    }
}

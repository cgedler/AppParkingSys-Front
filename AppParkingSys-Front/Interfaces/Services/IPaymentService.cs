using AppParkingSys_Front.Models;

namespace AppParkingSys_Front.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<Payment?> RegisterPayment(Payment payment, string token);
    }
}

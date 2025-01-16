using AppParkingSys_Front.Models;

namespace AppParkingSys_Front.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<Payment?> GetPaymentById(int id);
        Task<IEnumerable<Payment>> GetAll();
        Task<Payment> RegisterPayment(Payment payment);
        Task<Payment> UpdatePayment(int id, Payment payment);
        Task<Payment> DeletePayment(int id);
    }
}

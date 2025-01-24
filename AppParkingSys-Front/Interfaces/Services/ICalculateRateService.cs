using AppParkingSys_Front.Models;

namespace AppParkingSys_Front.Interfaces.Services
{
    public interface ICalculateRateService
    {
        Task<decimal?> CalculateRate(int id, string token, DateTime? EntryTime, DateTime? ExitTime);
    }
}

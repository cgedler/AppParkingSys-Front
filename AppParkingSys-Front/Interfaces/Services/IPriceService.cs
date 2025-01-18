using AppParkingSys_Front.Models;

namespace AppParkingSys_Front.Interfaces.Services
{
    public interface IPriceService
    {
        Task<List<Price>?> GetPricesAsync(string token);
        Task<Price?> GetPriceById(int id, string token);
        Task<Price?> RegisterPrice(Price price, string token);
        Task<Price?> UpdatePrice(int id, Price price, string token);
        Task<string?> DeletePrice(int id, string token);
    }
}

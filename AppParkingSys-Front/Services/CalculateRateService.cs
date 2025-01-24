using AppParkingSys_Front.Interfaces.Services;
using AppParkingSys_Front.Models;

namespace AppParkingSys_Front.Services
{
    public class CalculateRateService : ICalculateRateService
    {
        private readonly ILogger<CalculateRateService> _logger;
        private readonly IPriceService _priceService;
        public CalculateRateService(ILogger<CalculateRateService> logger, IPriceService priceService)
        { 
            _logger = logger;
            _priceService = priceService;
        }
        async Task<decimal?> ICalculateRateService.CalculateRate(int id, string token, DateTime? EntryTime, DateTime? ExitTime)
        {
            decimal result = 0m;
            decimal hours = 0m;
            if (EntryTime != null && ExitTime != null)
            {
                TimeSpan difference = (TimeSpan)(ExitTime - EntryTime);
                double hoursDifference = difference.TotalHours;
                hours = (decimal) hoursDifference;
            }
            var price = await _priceService.GetPriceById(id, token);
            if (price != null)
            {
                if (price.Amount != null)
                {
                    result = (decimal)price.Amount * hours;
                    return result;
                }
            }
            return result;
        }
    }
}

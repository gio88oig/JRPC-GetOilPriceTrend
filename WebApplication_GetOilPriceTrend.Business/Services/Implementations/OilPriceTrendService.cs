using Microsoft.Extensions.Logging;
using System.Globalization;
using WebApplication_GetOilPriceTrend.Business.Services.Interfaces;
using WebApplication_GetOilPriceTrend.DTO;
using WebApplication_GetOilPriceTrend.Models;

namespace WebApplication_GetOilPriceTrend.Business.Services.Implementations
{
    /// <summary>
    /// Service for working with Historical Oil Prices
    /// </summary>
    public class OilPriceTrendService : IOilPriceTrendService
    {
        private readonly ILogger<OilPriceTrendService> _logger;
        private readonly EuropeanBrentHistoricalPrice _europeanBrentHistoricalPrice;

        public OilPriceTrendService(ILogger<OilPriceTrendService> logger, EuropeanBrentHistoricalPrice europeanBrentHistoricalPrice) 
        {
            _logger = logger;
            _europeanBrentHistoricalPrice = europeanBrentHistoricalPrice;
        }

        /// <summary>
        /// Get historical oil prices filtered by input dates
        /// </summary>
        /// <param name="startDateISO8601"></param>
        /// <param name="endDateISO8601"></param>
        /// <returns>Oil price trend</returns>
        /// <exception cref="Exception"></exception>
        public async Task<OilPriceTrendDTO> GetOilPriceTrend(string startDateISO8601, string endDateISO8601)
        {
            DateTime startDate = DateTime.Now, endDate = DateTime.Now;

            // Input validation
            var validationMsg = validateInput(startDateISO8601, endDateISO8601, out startDate, out endDate);
            if (!string.IsNullOrEmpty(validationMsg))
            {
                _logger.LogError("Validation message: " + validationMsg);
                throw new Exception(validationMsg);
            }
            else
            {
                // Business logic (prices retrieval)
                try
                {
                    var historicalPricesFiltered = _europeanBrentHistoricalPrice.Prices.Where(p => p.Date >= startDate && p.Date <= endDate);
                    _logger.LogInformation("Historical data filtered");

                    return await Task.FromResult(new OilPriceTrendDTO()
                    {
                        prices = historicalPricesFiltered.Select(h => new OilPriceDTO { price = h.Price, dateISO8601 = h.Date.ToString("yyyy-MM-dd") })
                    });
                }
                catch (Exception ex) { throw new Exception("Error in service"); }
            }
            
        }

        /// <summary>
        /// Ensure startDateISO8601 and endDateISO8601 are valids and returns their representation in DateTime class
        /// </summary>
        /// <param name="startDateISO8601"></param>
        /// <param name="endDateISO8601"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns>In case of validation errors return error message, otherwise an empty message</returns>
        private string validateInput(string startDateISO8601, string endDateISO8601, out DateTime startDate, out DateTime endDate)
        {
            bool startValid = DateTime.TryParseExact(startDateISO8601, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);
            bool endValid = DateTime.TryParseExact(endDateISO8601, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);

            if (!(startValid && endValid)) return "Date not in ISO format";

            if (startDate > endDate) return "Start date cannot be greater than end date";

            return string.Empty;
        }
    }
}

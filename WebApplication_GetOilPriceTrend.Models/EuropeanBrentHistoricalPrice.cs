using System.Net.Http.Json;
using System.Text.Json;

namespace WebApplication_GetOilPriceTrend.Models
{
    /// <summary>
    /// Immutable object for storing Historical Oil Price
    /// </summary>
    public class EuropeanBrentHistoricalPrice
    {
        public const string EUROPEAN_BRENT_HISTORICAL_PRICE_JSON_URL = "https://glsitaly-download.s3.eu-central-1.amazonaws.com/MOBILE_APP/BrentDaily/brent-daily.json";
        
        // Once defined, Prices cannot be modified
        public IEnumerable<EuropeanBrentPrice> Prices { get; } = new List<EuropeanBrentPrice>();

        public EuropeanBrentHistoricalPrice(IEnumerable<EuropeanBrentPrice> price)
        {
            Prices = price;
        }

        /// <summary>
        /// Get historical oil prices by reading them from an online JSON file
        /// </summary>
        /// <returns></returns>
        public static async Task<EuropeanBrentHistoricalPrice> GetEuropeanBrentHistoricalPrice()
        {
            try
            {
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(EUROPEAN_BRENT_HISTORICAL_PRICE_JSON_URL);
                var result = await response.Content.ReadAsStringAsync();

                var historicalPrice = JsonSerializer.Deserialize<IEnumerable<EuropeanBrentPrice>>(result);

                if (historicalPrice == null) return new EuropeanBrentHistoricalPrice(new List<EuropeanBrentPrice>());

                return new EuropeanBrentHistoricalPrice(historicalPrice);
            }
            catch (Exception) { throw new Exception("Error in retriving historical oil prices from web url"); }
        }
    }

    public class EuropeanBrentPrice
    {
        public DateTime Date { get; set; }
        public double Price { get; set; } = 0;
    }
}

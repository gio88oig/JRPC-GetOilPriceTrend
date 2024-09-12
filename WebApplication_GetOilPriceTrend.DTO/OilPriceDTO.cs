namespace WebApplication_GetOilPriceTrend.DTO
{
    public class OilPriceDTO
    {
        public string dateISO8601 { get; set; } = string.Empty;
        public double price { get; set; } = 0;
    }
}

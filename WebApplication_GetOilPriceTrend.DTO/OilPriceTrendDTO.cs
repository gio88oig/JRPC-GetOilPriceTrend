namespace WebApplication_GetOilPriceTrend.DTO
{ 
    public class OilPriceTrendDTO
    {
        public IEnumerable<OilPriceDTO> prices { get; set; } = new List<OilPriceDTO>();
    }
}

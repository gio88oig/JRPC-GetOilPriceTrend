using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication_GetOilPriceTrend.Business.Services.Implementations;
using WebApplication_GetOilPriceTrend.DTO;
using WebApplication_GetOilPriceTrend.Models;
using Xunit;

namespace WebApplication_GetOilPriceTrend.Tests
{
    public class OilPriceTrendServiceTests
    {
        private readonly Mock<ILogger<OilPriceTrendService>> _loggerMock;
        private readonly EuropeanBrentHistoricalPrice _europeanBrentHistoricalPrice;

        public OilPriceTrendServiceTests()
        {
            _loggerMock = new Mock<ILogger<OilPriceTrendService>>();
            _europeanBrentHistoricalPrice = new EuropeanBrentHistoricalPrice(
                new List<EuropeanBrentPrice>
                {
                    new EuropeanBrentPrice { Date = new DateTime(2022, 1, 1), Price = 50 },
                    new EuropeanBrentPrice { Date = new DateTime(2022, 1, 2), Price = 55 },
                    new EuropeanBrentPrice { Date = new DateTime(2022, 1, 3), Price = 60 },
                    new EuropeanBrentPrice { Date = new DateTime(2022, 1, 4), Price = 65 },
                    new EuropeanBrentPrice { Date = new DateTime(2022, 1, 5), Price = 70 }
                }
             );
        }

        [Fact]
        public async Task GetOilPriceTrend_ValidInput_ReturnsFilteredPrices()
        {
            // Arrange
            var service = new OilPriceTrendService(_loggerMock.Object, _europeanBrentHistoricalPrice);
            var startDate = "2022-01-02";
            var endDate = "2022-01-04";

            // Act
            var result = await service.GetOilPriceTrend(startDate, endDate);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.prices);
            Assert.Equal(3, result.prices.Count());

            var expectedPrices = new List<OilPriceDTO>
            {
                new OilPriceDTO { price = 55, dateISO8601 = "2022-01-02" },
                new OilPriceDTO { price = 60, dateISO8601 = "2022-01-03" },
                new OilPriceDTO { price = 65, dateISO8601 = "2022-01-04" }
            };
            Assert.Equivalent(expectedPrices, result.prices.ToList());
        }

        [Fact]
        public async Task GetOilPriceTrend_InvalidInput_ThrowsException()
        {
            // Arrange
            var service = new OilPriceTrendService(_loggerMock.Object, _europeanBrentHistoricalPrice);
            var startDate = "2022-01-05";
            var endDate = "2022-01-02";

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => service.GetOilPriceTrend(startDate, endDate));
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using WebApplication_GetOilPriceTrend.Business.Services.Implementations;
using WebApplication_GetOilPriceTrend.Business.Services.Interfaces;
using WebApplication_GetOilPriceTrend.Models;

namespace WebApplication_GetOilPriceTrend.Business.Extensions
{
    public static class ServicesExtensions
    {
        public static async Task AddBusinessServices(this IServiceCollection services)
        {
            services.AddTransient<IOilPriceTrendService, OilPriceTrendService>();
            
            // Immutable object is declared as singleton and can be used within services
            services.AddSingleton(await EuropeanBrentHistoricalPrice.GetEuropeanBrentHistoricalPrice());
        }

    }
}

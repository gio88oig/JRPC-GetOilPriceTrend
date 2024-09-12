using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication_GetOilPriceTrend.DTO;

namespace WebApplication_GetOilPriceTrend.Business.Services.Interfaces
{
    public interface IOilPriceTrendService
    {
        public Task<OilPriceTrendDTO> GetOilPriceTrend(string startDateISO8601, string endDateISO8601);
    }
}

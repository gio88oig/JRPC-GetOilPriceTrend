using EdjCase.JsonRpc.Router;
using EdjCase.JsonRpc.Router.Abstractions;
using Microsoft.AspNetCore.Mvc;
using WebApplication_GetOilPriceTrend.Business.Services.Interfaces;
using WebApplication_GetOilPriceTrend.DTO;

namespace WebApplication_GetOilPriceTrend.Controllers
{
    /// <summary>
    /// RPC Controller for defining API
    /// </summary>
    [RpcRoute("")]
    public class OilPriceTrendController : RpcController
    {
        private readonly ILogger<OilPriceTrendController> _logger;
        private readonly IOilPriceTrendService _oilPriceTrendService;

        public OilPriceTrendController(ILogger<OilPriceTrendController> logger, IOilPriceTrendService oilPriceTrendService)
        {
            _logger = logger;
            _oilPriceTrendService = oilPriceTrendService;
        }

        /// <summary>
        /// API for filtering and obtaining historical oil prices
        /// </summary>
        /// <param name="startDateISO8601"></param>
        /// <param name="endDateISO8601"></param>
        /// <returns></returns>
        public async Task<IRpcMethodResult> GetOilPriceTrend(string startDateISO8601, string endDateISO8601)
        {
            try
            {
                return this.Ok(await _oilPriceTrendService.GetOilPriceTrend(startDateISO8601, endDateISO8601));
            }
            catch (Exception ex)
            {
                return this.Error(500, ex.Message);
            }
            
        }
    }
}

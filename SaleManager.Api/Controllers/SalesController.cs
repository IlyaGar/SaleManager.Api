using Microsoft.AspNetCore.Mvc;
using SaleManager.Api.BLL.Service.Interface.Sales.Contracts.InComing;
using SaleManager.Api.BLL.Service.Interface.Sales.Interfaces;

namespace SaleManager.Api.Controllers
{
    [ApiController]
    [Route("api/sales")]
    public class SalesController(ISaleService saleService) : ControllerBase
    {
        private readonly ISaleService _saleService = saleService;

        /// <summary>
        /// Get data for graph
        /// </summary>
        /// <param name="newLicense">Payload with filter</param>
        [HttpPost]
        [Route("get-chart")]
        public async Task<IActionResult> GetAggregatedSales(SaleFilterInContract filter)
        {
            return Ok(await _saleService.GetAggregatedSalesAsync(filter));
        }


        /// <summary>
        /// Create sale
        /// </summary>
        /// <param name="createSale">Payload with sale</param>
        [HttpPost]
        [Route("sale")]
        public async Task<IActionResult> CreateSale(CreateSaleInContract createSale)
        {
            await _saleService.CreateSaleAsync(createSale);

            return Ok();
        }
    }
}

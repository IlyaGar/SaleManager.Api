using SaleManager.Api.BLL.Service.Interface.Sales.Contracts.InComing;
using SaleManager.Api.BLL.Service.Interface.Sales.Contracts.OutGoing;

namespace SaleManager.Api.BLL.Service.Interface.Sales.Interfaces
{
    public interface ISaleService
    {
        Task CreateSaleAsync(CreateSaleInContract createSale);
        Task<IEnumerable<AggregatedSaleOutContarct>> GetAggregatedSalesAsync(SaleFilterInContract filter);
    }
}

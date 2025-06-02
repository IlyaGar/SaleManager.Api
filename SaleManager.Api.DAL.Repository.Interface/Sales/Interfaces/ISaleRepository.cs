using SaleManager.Api.DAL.Repository.Interface.Sales.Models;

namespace SaleManager.Api.DAL.Repository.Interface.Sales.Interfaces
{
    public interface ISaleRepository
    {
        Task<IEnumerable<Sale>> GetSaleCollectionAsync(DateTimeOffset start, DateTimeOffset end);
        Task CreateSaleAsync(Sale sale);
    }
}

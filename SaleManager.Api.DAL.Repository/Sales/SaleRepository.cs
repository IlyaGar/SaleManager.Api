using Microsoft.EntityFrameworkCore;
using SaleManager.Api.DAL.Repository.Context;
using SaleManager.Api.DAL.Repository.Interface.Sales.Interfaces;
using SaleManager.Api.DAL.Repository.Interface.Sales.Models;

namespace SaleManager.Api.DAL.Repository.Sales
{
    public class SaleRepository(SaleManagerDbContext context) : ISaleRepository
    {
        private readonly SaleManagerDbContext _context = context;

        public async Task CreateSaleAsync(Sale sale)
        {
            _context.Sale.Add(sale);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Sale>> GetSaleCollectionAsync(DateTimeOffset start, DateTimeOffset end)
        {
            return await _context.Sale
                .Where(s => s.SaleDateTime >= start && s.SaleDateTime <= end)
                .ToListAsync();
        }
    }
}

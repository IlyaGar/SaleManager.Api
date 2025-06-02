using SaleManager.Api.BLL.Service.Interface.Common.Models;
using SaleManager.Api.BLL.Service.Interface.Sales.Contracts.InComing;
using SaleManager.Api.BLL.Service.Interface.Sales.Contracts.OutGoing;
using SaleManager.Api.BLL.Service.Interface.Sales.Interfaces;
using SaleManager.Api.DAL.Repository.Interface.Sales.Interfaces;
using SaleManager.Api.DAL.Repository.Interface.Sales.Models;

namespace SaleManager.Api.BLL.Service.Sales
{
    public class SaleService(ISaleRepository saleRepository) : ISaleService
    {
        private readonly ISaleRepository _saleRepository = saleRepository;

        public async Task CreateSaleAsync(CreateSaleInContract createSale)
        {
            await _saleRepository.CreateSaleAsync(new Sale { SaleDateTime = createSale.SaleDateTime, Amount = createSale.Amount });
        }

        public async Task<IEnumerable<AggregatedSaleOutContarct>> GetAggregatedSalesAsync(SaleFilterInContract filter)
        {
            var sales = await _saleRepository.GetSaleCollectionAsync(filter.StartDate, filter.EndDate);

            var groupedSales = sales.GroupBy(sale => GetPeriodStart(sale.SaleDateTime, filter.IntervalType)).OrderBy(g => g.Key);

            var result = groupedSales.Select(g => new AggregatedSaleOutContarct
            {
                PeriodStart = g.Key,
                IntervalType = filter.IntervalType,
                TotalCount = g.Count(),
                SumInThousands = (int)(g.Sum(s => s.Amount) / 10 /*1000*/)
            }).ToList();
            
            return result;
        }

        private static DateTimeOffset GetPeriodStart(DateTimeOffset date, IntervalType intervalType)
        {
            return intervalType switch
            {
                IntervalType.Day => date.Date,
                IntervalType.Week => GetStartOfWeek(date),
                IntervalType.Month => new DateTimeOffset(date.Year, date.Month, 1, 0, 0, 0, date.Offset),
                IntervalType.Quarter => GetStartOfQuarter(date),
                _ => date.Date
            };
        }

        private static DateTimeOffset GetStartOfWeek(DateTimeOffset date)
        {
            var diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
            return date.AddDays(-diff).Date;
        }

        private static DateTimeOffset GetStartOfQuarter(DateTimeOffset date)
        {
            int currentQuarter = ((date.Month - 1) / 3) + 1;
            int firstMonthOfQuarter = (currentQuarter - 1) * 3 + 1;
            return new DateTimeOffset(date.Year, firstMonthOfQuarter, 1, 0, 0, 0, date.Offset);
        }
    }
}

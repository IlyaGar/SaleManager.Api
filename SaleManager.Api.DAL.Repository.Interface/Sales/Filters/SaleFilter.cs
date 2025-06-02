namespace SaleManager.Api.DAL.Repository.Interface.Sales.Filters
{
    public class SaleFilter
    {
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
    }
}

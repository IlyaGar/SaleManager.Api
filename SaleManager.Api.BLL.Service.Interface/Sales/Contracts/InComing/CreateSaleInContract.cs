namespace SaleManager.Api.BLL.Service.Interface.Sales.Contracts.InComing
{
    public class CreateSaleInContract
    {
        public DateTimeOffset SaleDateTime { get; set; }
        public decimal Amount { get; set; }
    }
}

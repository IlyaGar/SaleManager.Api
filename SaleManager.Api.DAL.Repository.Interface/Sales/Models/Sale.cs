namespace SaleManager.Api.DAL.Repository.Interface.Sales.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public DateTimeOffset SaleDateTime { get; set; }
        public decimal Amount { get; set; }
    }
}

using SaleManager.Api.BLL.Service.Interface.Common.Models;
using System.Text.Json.Serialization;

namespace SaleManager.Api.BLL.Service.Interface.Sales.Contracts.InComing
{
    public class SaleFilterInContract
    {
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public IntervalType IntervalType { get; set; }  // "day", "week", "month", "quarter"
    }
}

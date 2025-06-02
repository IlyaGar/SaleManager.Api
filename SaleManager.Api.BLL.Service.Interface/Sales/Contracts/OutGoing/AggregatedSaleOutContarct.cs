using SaleManager.Api.BLL.Service.Interface.Common.Models;
using System.Text.Json.Serialization;

namespace SaleManager.Api.BLL.Service.Interface.Sales.Contracts.OutGoing
{
    public class AggregatedSaleOutContarct
    {
        public DateTimeOffset PeriodStart { get; set; }
        public int SumInThousands { get; set; }
        public int TotalCount { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public IntervalType IntervalType { get; set; }  // "day", "week", "month", "quarter"
    }
}

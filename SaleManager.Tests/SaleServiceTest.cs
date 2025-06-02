using FluentAssertions;
using Moq;
using SaleManager.Api.DAL.Repository.Interface.Sales.Interfaces;
using SaleManager.Api.BLL.Service.Sales;
using SaleManager.Api.BLL.Service.Interface.Sales.Contracts.InComing;
using NUnit.Framework;
using SaleManager.Api.DAL.Repository.Interface.Sales.Models;
using SaleManager.Api.BLL.Service.Interface.Common.Models;

public class SaleServiceTests
{
    private readonly Mock<ISaleRepository> _saleRepositoryMock;
    private readonly SaleService _saleService;

    public SaleServiceTests()
    {
        _saleRepositoryMock = new Mock<ISaleRepository>();
        _saleService = new SaleService(_saleRepositoryMock.Object);
    }

    [Test]
    public async Task CreateSaleAsync_CreatesSale()
    {
        // Arrange
        var input = new CreateSaleInContract
        {
            SaleDateTime = DateTimeOffset.Now,
            Amount = 100
        };

        // Act
        await _saleService.CreateSaleAsync(input);

        // Assert
        _saleRepositoryMock.Verify(repo =>
            repo.CreateSaleAsync(It.Is<Sale>(s =>
                s.SaleDateTime == input.SaleDateTime && s.Amount == input.Amount)),
            Times.Once);
    }

    [Test]
    public async Task GetAggregatedSalesAsync_ReturnsGroupedSales()
    {
        // Arrange
        var sales = new List<Sale>
        {
            new Sale { SaleDateTime = new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.FromHours(3)), Amount = 100 },
            new Sale { SaleDateTime = new DateTimeOffset(2025, 1, 1, 12, 0, 0, TimeSpan.FromHours(3)), Amount = 200 },
            new Sale { SaleDateTime = new DateTimeOffset(2025, 1, 2, 0, 0, 0, TimeSpan.FromHours(3)), Amount = 300 }
        };

        var filter = new SaleFilterInContract
        {
            StartDate = new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.FromHours(3)),
            EndDate = new DateTimeOffset(2025, 1, 3, 0, 0, 0, TimeSpan.FromHours(3)),
            IntervalType = IntervalType.Day
        };

        _saleRepositoryMock
            .Setup(repo => repo.GetSaleCollectionAsync(filter.StartDate, filter.EndDate))
            .ReturnsAsync(sales);

        // Act
        var result = await _saleService.GetAggregatedSalesAsync(filter);

        // Assert
        result.Should().HaveCount(2);
        result.Should().Contain(r => r.PeriodStart == new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.FromHours(3)) && r.TotalCount == 2 && r.SumInThousands == 30);
        result.Should().Contain(r => r.PeriodStart == new DateTimeOffset(2025, 1, 2, 0, 0, 0, TimeSpan.FromHours(3)) && r.TotalCount == 1 && r.SumInThousands == 30);
    }
}

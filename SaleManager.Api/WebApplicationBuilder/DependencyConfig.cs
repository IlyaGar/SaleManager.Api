using SaleManager.Api.BLL.Service.Interface.Sales.Interfaces;
using SaleManager.Api.BLL.Service.Sales;
using SaleManager.Api.DAL.Repository.Interface.Sales.Interfaces;
using SaleManager.Api.DAL.Repository.Sales;

namespace SaleManager.Api.WebApplicationBuilder
{
    internal static class DependencyConfig
    {
        internal static void ConfigureServices(IServiceCollection services)
        {
            ConfigureBllServices(services);
            ConfigureDalRepository(services);
        }

        private static void ConfigureBllServices(IServiceCollection services)
        {
            services.AddScoped<ISaleService, SaleService>();
        }

        private static void ConfigureDalRepository(IServiceCollection services)
        {
            services.AddScoped<ISaleRepository, SaleRepository>();
        }
    }
}
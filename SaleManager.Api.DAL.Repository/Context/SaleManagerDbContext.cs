using Microsoft.EntityFrameworkCore;
using SaleManager.Api.DAL.Repository.Interface.Sales.Models;

namespace SaleManager.Api.DAL.Repository.Context
{
    public class SaleManagerDbContext : DbContext
    {
        public DbSet<Sale> Sale {  get; set; }

        public SaleManagerDbContext(DbContextOptions<SaleManagerDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}

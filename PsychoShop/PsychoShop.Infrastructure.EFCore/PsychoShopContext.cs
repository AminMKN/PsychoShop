using Microsoft.EntityFrameworkCore;
using PsychoShop.Domain.AccountAgg;
using PsychoShop.Domain.ProductCategoryAgg;
using PsychoShop.Infrastructure.EFCore.Mapping;

namespace PsychoShop.Infrastructure.EFCore
{
    public class PsychoShopContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        public PsychoShopContext(DbContextOptions<PsychoShopContext> context) : base(context)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(ProductCategoryMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}

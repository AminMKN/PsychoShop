using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PsychoShop.Domain.ProductCategoryAgg;
using PsychoShop.Domain.UserAccountAgg;
using PsychoShop.Infrastructure.EFCore.Mapping;

namespace PsychoShop.Infrastructure.EFCore
{
    public class PsychoShopContext : IdentityDbContext<UserAccount>
    {
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

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PsychoShop.Domain.SpecialProductAgg;

namespace ShopManagement.Infrastructure.EFCore.Mapping
{
    public class SpecialProductMapping : IEntityTypeConfiguration<SpecialProduct>
    {
        public void Configure(EntityTypeBuilder<SpecialProduct> builder)
        {
            builder.ToTable("SpecialProducts");
            builder.HasKey(x => x.Id);

            builder
                .HasOne(x => x.Product)
                .WithMany(x => x.SpecialProducts)
                .HasForeignKey(x => x.ProductId);
        }
    }
}

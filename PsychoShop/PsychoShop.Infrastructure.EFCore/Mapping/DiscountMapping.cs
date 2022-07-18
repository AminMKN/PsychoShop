using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PsychoShop.Domain.DiscountAgg;

namespace PsychoShop.Infrastructure.EFCore.Mapping
{
    public class DiscountMapping : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.ToTable("Discounts");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Reason).HasMaxLength(500).IsRequired();

            builder
                .HasOne(x => x.Product)
                .WithMany(x => x.Discounts)
                .HasForeignKey(x => x.ProductId);
        }
    }
}

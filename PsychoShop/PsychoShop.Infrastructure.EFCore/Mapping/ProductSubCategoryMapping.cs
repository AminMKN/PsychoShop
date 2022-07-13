﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PsychoShop.Domain.ProductSubCategoryAgg;

namespace PsychoShop.Infrastructure.EFCore.Mapping
{
    public class ProductSubCategoryMapping : IEntityTypeConfiguration<ProductSubCategory>
    {
        public void Configure(EntityTypeBuilder<ProductSubCategory> builder)
        {
            builder.ToTable("ProductSubCategories");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Slug).HasMaxLength(300).IsRequired();
            builder.Property(x => x.Keywords).HasMaxLength(80).IsRequired();
            builder.Property(x => x.MetaDescription).HasMaxLength(150).IsRequired();

            builder
                .HasOne(x => x.ProductCategory)
                .WithMany(x => x.ProductSubCategories)
                .HasForeignKey(x => x.ProductCategoryId);

            builder
                .HasMany(x => x.Products)
                .WithOne(x => x.ProductSubCategory)
                .HasForeignKey(x => x.ProductSubCategoryId);
        }
    }
}

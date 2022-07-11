using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PsychoShop.Domain.UserAccountAgg;

namespace PsychoShop.Infrastructure.EFCore.Mapping
{
    public class UserAccountMapping : IEntityTypeConfiguration<UserAccount>
    {
        public void Configure(EntityTypeBuilder<UserAccount> builder)
        {
            builder.Property(x => x.FullName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.ProfilePhoto).HasMaxLength(1000);
        }
    }
}

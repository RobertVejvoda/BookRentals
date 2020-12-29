using BookRentals.Membership.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookRentals.Membership.Infrastructure.Configuration
{
    public class AddressEntityConfiguration : IEntityTypeConfiguration<AddressEntity>
    {
        public void Configure(EntityTypeBuilder<AddressEntity> builder)
        {
            builder.ToTable("Address");
            builder.HasKey(p => p.Id).IsClustered();
            builder.Property(p => p.Ident).UseIdentityColumn();
            builder.Property(p => p.Address1).HasMaxLength(128).IsUnicode().IsRequired();
            builder.Property(p => p.Address2).HasMaxLength(128).IsUnicode().IsRequired(false);
            builder.Property(p => p.City).HasMaxLength(64).IsUnicode().IsRequired();
            builder.Property(p => p.Country).HasMaxLength(64).IsUnicode().IsRequired();
            builder.Property(p => p.Zip).HasMaxLength(16).IsUnicode(false).IsRequired();
            builder.Property(p => p.ModifiedById).IsRequired();
            builder.Property(p => p.ModifiedOn).HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
            builder.Property(p => p.Version).IsRowVersion();
        }
    }
}

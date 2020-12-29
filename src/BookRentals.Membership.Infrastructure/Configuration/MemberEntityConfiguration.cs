using BookRentals.Membership.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookRentals.Membership.Infrastructure.Configuration
{
    public class MemberEntityConfiguraration : IEntityTypeConfiguration<MemberEntity>
    {
        public void Configure(EntityTypeBuilder<MemberEntity> builder)
        {
            builder.ToTable("Member");
            builder.HasKey(p => p.Id).IsClustered();
            builder.Property(p => p.MemberId).UseIdentityColumn();
            builder.Property(p => p.FirstName).HasMaxLength(64).IsRequired();
            builder.Property(p => p.LastName).HasMaxLength(64).IsRequired();
            builder.Property(p => p.Email).HasMaxLength(128).IsRequired();
            builder.Property(p => p.Mobile).HasMaxLength(16).IsRequired();
            builder.Property(p => p.ModifiedById).IsRequired();
            builder.Property(p => p.ModifiedOn).HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
            builder.Property(p => p.Version).IsRowVersion();

            builder.HasOne(p => p.MainAddress)
               .WithOne(a => a.Member)
               .HasForeignKey<MemberAddressEntity>(a => a.MainAddressId)
               .IsRequired();

            builder.HasOne(p => p.ShippingAddress)
                .WithOne(a => a.Member)
                .HasForeignKey<MemberAddressEntity>(a => a.ShippingAddressId)
                .IsRequired(false);

            builder.HasOne(p => p.BillingAddress)
                .WithOne(a => a.Member)
                .HasForeignKey<MemberAddressEntity>(a => a.BillingAddressId)
                .IsRequired(false);

            builder.HasMany(p => p.Memberships)
                .WithOne(ms => ms.Member)
                .IsRequired(true);
        }
    }
}

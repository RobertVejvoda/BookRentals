using BookRentals.Mediotheca.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookRentals.Mediotheca.Infrastructure.Configuration
{
    public class LibraryEntityConfiguration : IEntityTypeConfiguration<LibraryEntity>
    {
        public void Configure(EntityTypeBuilder<LibraryEntity> builder)
        {
            builder.ToTable("Library");
            builder.Property(p => p.Ident).UseIdentityColumn();
            builder.Property(p => p.Name).IsUnicode().HasMaxLength(255).IsRequired();
            builder.Property(p => p.ModifiedById).IsRequired();
            builder.Property(p => p.ModifiedOn).HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
            builder.Property(p => p.Version).IsRowVersion();

            builder.HasOne(p => p.MainAddress)
                .WithOne(a => a.Library)
                .HasForeignKey<LibraryAddressEntity>(a => a.MainAddressId);

            builder.HasOne(p => p.ShippingAddress)
                .WithOne(a => a.Library)
                .HasForeignKey<LibraryAddressEntity>(a => a.ShippingAddressId);

            builder.HasOne(p => p.BillingAddress)
                .WithOne(a => a.Library)
                .HasForeignKey<LibraryAddressEntity>(a => a.BillingAddressId);
        }
    }
}

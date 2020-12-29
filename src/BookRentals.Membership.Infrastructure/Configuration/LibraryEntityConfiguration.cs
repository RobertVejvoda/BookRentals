using BookRentals.Membership.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookRentals.Membership.Infrastructure.Configuration
{
    public class LibraryEntityConfiguration : IEntityTypeConfiguration<LibraryEntity>
    {
        public void Configure(EntityTypeBuilder<LibraryEntity> builder)
        {
            builder.ToTable("Library");
            builder.HasKey(p => p.Id).IsClustered();
            builder.Property(p => p.Name).HasMaxLength(128).IsUnicode().IsRequired();
            builder.Property(p => p.Address).HasMaxLength(1024).IsUnicode().IsRequired();
        }
    }
}

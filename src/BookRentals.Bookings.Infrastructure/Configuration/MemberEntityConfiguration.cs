using BookRentals.Bookings.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookRentals.Bookings.Infrastructure.Configuration
{
    public class MemberEntityConfiguration : IEntityTypeConfiguration<MemberEntity>
    {
        public void Configure(EntityTypeBuilder<MemberEntity> builder)
        {
            builder.ToTable("Member");
            builder.HasKey(p => p.Id).IsClustered();
            builder.Property(p => p.FullName).IsUnicode().HasMaxLength(128).IsRequired();
            builder.Property(p => p.Email).IsUnicode().HasMaxLength(128).IsRequired();
            builder.Property(p => p.Mobile).IsUnicode().HasMaxLength(128).IsRequired();
            builder.Property(p => p.ActiveMembership).IsUnicode().HasMaxLength(128).IsRequired(false);
        }
    }
}

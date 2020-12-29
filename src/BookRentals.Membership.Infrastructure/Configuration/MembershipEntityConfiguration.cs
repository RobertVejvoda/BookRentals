using BookRentals.Membership.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookRentals.Membership.Infrastructure.Configuration
{
    public class MembershipEntityConfiguration : IEntityTypeConfiguration<MembershipEntity>
    {
        public void Configure(EntityTypeBuilder<MembershipEntity> builder)
        {
            builder.ToTable("Membership");
            builder.HasKey(p => p.Id).IsClustered();
            builder.Property(p => p.Ident).UseIdentityColumn();
            builder.Property(p => p.MembershipId).UseIdentityColumn();
            builder.Property(p => p.MemberFrom).IsRequired();
            builder.Property(p => p.MemberTo).IsRequired();
            builder.Property(p => p.ModifiedById).IsRequired();
            builder.Property(p => p.ModifiedOn).HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
            builder.Property(p => p.Version).IsRowVersion();

            builder.HasOne(p => p.Member)
                .WithMany(m => m.Memberships)
                .HasForeignKey(p => p.Id)
                .IsRequired();
        }
    }
}

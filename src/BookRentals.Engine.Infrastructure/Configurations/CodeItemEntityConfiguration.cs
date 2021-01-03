using BookRentals.Engine.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookRentals.Engine.Infrastructure.Configurations
{
    public class CodeItemEntityConfiguration : IEntityTypeConfiguration<CodeItemEntity>
    {
        public void Configure(EntityTypeBuilder<CodeItemEntity> builder)
        {
            builder.ToTable("CodeItem");
            builder.HasKey(p => p.Id).IsClustered();
            builder.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
            builder.Property(p => p.Ident).ValueGeneratedOnAdd().UseIdentityColumn().Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);
            builder.Property(p => p.CodeItemRef).IsUnicode(false).HasMaxLength(64).IsRequired();
            builder.Property(p => p.Caption).IsUnicode().HasMaxLength(128).IsRequired();
            builder.Property(p => p.Symbol).IsUnicode().HasMaxLength(3).IsFixedLength(true);
            builder.Property(p => p.Description).IsUnicode().HasMaxLength(512).IsRequired(false);
            builder.Property(p => p.ModifiedById).IsRequired();
            builder.Property(p => p.ModifiedOn).HasPrecision(3).HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
            builder.Property(p => p.Version).IsRowVersion();
        }
    }
}

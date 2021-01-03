using BookRentals.Core.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookRentals.Engine.Infrastructure.Configurations
{
    public class LanguageEntityConfiguration : IEntityTypeConfiguration<LanguageEntity>
    {
        public void Configure(EntityTypeBuilder<LanguageEntity> builder)
        {
            builder.ToTable("Language");
            builder.HasKey(p => p.Id).IsClustered();
            builder.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
            builder.Property(p => p.Ident).ValueGeneratedOnAdd().UseIdentityColumn().Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);
            builder.Property(p => p.Caption).HasMaxLength(64).IsRequired();
            builder.Property(p => p.CultureCode).IsFixedLength(true).HasMaxLength(5).IsUnicode(false).IsRequired(false);
            builder.Property(p => p.ModifiedById).IsRequired();
            builder.Property(p => p.ModifiedOn).HasPrecision(3).HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
            builder.Property(p => p.Version).IsRowVersion();
        }
    }
}

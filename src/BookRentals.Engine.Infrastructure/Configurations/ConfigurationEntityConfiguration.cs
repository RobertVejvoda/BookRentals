using BookRentals.Engine.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookRentals.Engine.Infrastructure.Configurations
{
    public class ConfigurationEntityConfiguration : IEntityTypeConfiguration<ConfigurationEntity>
    {
        public void Configure(EntityTypeBuilder<ConfigurationEntity> builder)
        {
            builder.ToTable("Configuration");
            builder.HasKey(p => p.Id).IsClustered();
            builder.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
            builder.Property(p => p.Ident).ValueGeneratedOnAdd().UseIdentityColumn().Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);
            builder.Property(p => p.Reference).IsUnicode(false).HasMaxLength(64).IsRequired();
            builder.Property(p => p.Caption).IsUnicode().HasMaxLength(128).IsRequired();
            builder.Property(p => p.Description).IsUnicode().HasMaxLength(512).IsRequired(false);
            builder.Property(p => p.ConfigurationKey).HasDefaultValueSql("'*'").IsUnicode(false).HasMaxLength(32).IsRequired();
            builder.Property(p => p.ModifiedById).IsRequired();
            builder.Property(p => p.ModifiedOn).HasPrecision(3).HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
            builder.Property(p => p.Version).IsRowVersion();

            builder.HasIndex(ux => new { ux.Reference, ux.ConfigurationKey }).IsUnique(true);
        }
    }
}

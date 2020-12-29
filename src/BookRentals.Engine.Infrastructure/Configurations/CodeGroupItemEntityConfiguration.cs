using BookRentals.Engine.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookRentals.Engine.Infrastructure.Configurations
{
    public class CodeGroupItemEntityConfiguration : IEntityTypeConfiguration<CodeGroupItemEntity>
    {
        public void Configure(EntityTypeBuilder<CodeGroupItemEntity> builder)
        {
            builder.ToTable("CodeGroupItem");
            builder.HasKey(p => new { p.CodeGroupId, p.CodeItemId });
            builder.Property(p => p.Ident).UseIdentityColumn();
            builder.Property(p => p.ConfigurationKey).HasDefaultValueSql("'*'").IsUnicode(false).HasMaxLength(32).IsRequired();
            builder.Property(p => p.DisplayOrder).HasDefaultValue(0).IsRequired();
            builder.Property(p => p.IsDisabled).HasDefaultValue(false).IsRequired();

            builder.HasOne(p => p.CodeGroup).WithMany(cg => cg.CodeGroupItems).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.CodeItem).WithMany(ci => ci.CodeGroupItems).OnDelete(DeleteBehavior.Restrict);
        }
    }
}

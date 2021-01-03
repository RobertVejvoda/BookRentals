using BookRentals.Core.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookRentals.Engine.Infrastructure.Configurations
{
    public class DeletedEntityConfiguration : IEntityTypeConfiguration<DeletedEntity>
    {
        public void Configure(EntityTypeBuilder<DeletedEntity> builder)
        {
            builder.ToTable("Deletes");
            builder.HasKey(p => p.Id).IsClustered();
            builder.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
            builder.Property(p => p.EntityId);
            builder.Property(p => p.EntityName).HasMaxLength(64).IsUnicode(false).IsRequired(true);
            builder.Property(p => p.DeletedById).IsRequired();
            builder.Property(p => p.DeletedOn).HasPrecision(3).HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
        }
    }
}

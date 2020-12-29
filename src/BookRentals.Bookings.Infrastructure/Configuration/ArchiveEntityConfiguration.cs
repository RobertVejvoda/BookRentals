using BookRentals.Bookings.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRentals.Bookings.Infrastructure.Configuration
{
    public class ArchiveEntityConfiguration : IEntityTypeConfiguration<ArchiveEntity>
    {
        public void Configure(EntityTypeBuilder<ArchiveEntity> builder)
        {
            builder.ToTable("Archive");
            builder.HasKey(p => p.Id).IsClustered();
            builder.Property(p => p.Ident).UseIdentityColumn();
            builder.Property(p => p.Title).IsUnicode(true).HasMaxLength(128);
            builder.Property(p => p.ModifiedById).IsRequired();
            builder.Property(p => p.ModifiedOn).HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
            builder.Property(p => p.Version).IsRowVersion();
        }
    }
}

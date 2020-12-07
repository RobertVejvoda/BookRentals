using BookRentals.Core.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRentals.Core.Infrastructure.Configurations
{
    public class CodeItemEntityConfiguration : IEntityTypeConfiguration<CodeItemEntity>
    {
        public void Configure(EntityTypeBuilder<CodeItemEntity> builder)
        {
            builder.HasKey(p => p.Id).IsClustered();
            builder.Property(p => p.CodeItemId).UseIdentityColumn();
            builder.Property(p => p.CodeItemRef).IsUnicode(false).HasMaxLength(64);
            builder.Property(p => p.Caption).IsUnicode().HasMaxLength(128).IsRequired();
            builder.Property(p => p.Description).IsUnicode().IsRequired(false);
            builder.Property(p => p.CreatedById).IsRequired(true);
            builder.Property(p => p.CreatedDate);
            builder.Property(p => p.Version).IsRowVersion();
        }
    }
}

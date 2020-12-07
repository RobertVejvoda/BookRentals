using BookRentals.Membership.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRentals.Membership.Infrastructure.Configuration
{
    public class LibraryEntityConfiguration : IEntityTypeConfiguration<LibraryEntity>
    {
        public void Configure(EntityTypeBuilder<LibraryEntity> builder)
        {
            builder.ToTable("Library").;

            builder.OwnsOne(p => p.MainAddress, a => a.Property(p => p.);
            builder.OwnsOne(p => p.ShippingAddress);
        }
    }
}

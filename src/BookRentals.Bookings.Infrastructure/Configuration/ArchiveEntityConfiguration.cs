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
        }
    }
}

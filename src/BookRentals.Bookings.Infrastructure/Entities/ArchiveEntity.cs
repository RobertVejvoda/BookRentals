using BookRentals.Core.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRentals.Bookings.Infrastructure.Entities
{
    public class ArchiveEntity : AuditableEntity
    {
        public string Title { get; set; }
    }
}

using BookRentals.Core.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRentals.Membership.Infrastructure.Entities
{

    public class LibraryEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}

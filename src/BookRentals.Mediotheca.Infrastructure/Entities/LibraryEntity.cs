using BookRentals.Core.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRentals.Mediotheca.Infrastructure.Entities
{
    public class LibraryEntity : AuditableEntity
    {
        public string Name { get; set; }
        public virtual LibraryAddressEntity MainAddress { get; set; }
        public virtual LibraryAddressEntity ShippingAddress { get; set; }
        public virtual LibraryAddressEntity BillingAddress { get; set; }
    }
}

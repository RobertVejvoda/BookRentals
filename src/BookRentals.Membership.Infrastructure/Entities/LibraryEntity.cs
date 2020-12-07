using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRentals.Membership.Infrastructure.Entities
{
    
    public class LibraryEntity : Entity<Guid>
    {
        
        public string Name { get; set; }
        public AddressEntity MainAddress { get; set; }
        public AddressEntity ShippingAddress { get; set; }
    }
}

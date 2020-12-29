using BookRentals.Core.Infrastructure.Entities;
using BookRentals.Core.Versioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRentals.Membership.Infrastructure.Entities
{
    public class MemberEntity : AuditableEntity
    {
        public int MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }

        public MemberAddressEntity MainAddress { get; set; }
        public MemberAddressEntity ShippingAddress { get; set; }
        public MemberAddressEntity BillingAddress { get; set; }

        public ICollection<MembershipEntity> Memberships { get; set; }

    }
}

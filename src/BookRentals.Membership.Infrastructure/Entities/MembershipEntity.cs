using BookRentals.Core.Infrastructure.Entities;
using BookRentals.Core.Versioning;
using System;

namespace BookRentals.Membership.Infrastructure.Entities
{
    public class MembershipEntity : AuditableEntity
    {
        public int MembershipId { get; set; }
        public DateTime MemberFrom { get; set; }
        public DateTime MemberTo { get; set; }

        public MemberEntity Member { get; set; }
    }
}

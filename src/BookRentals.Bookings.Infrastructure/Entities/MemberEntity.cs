using System;

namespace BookRentals.Bookings.Infrastructure.Entities
{
    public class MemberEntity
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string ActiveMembership { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BookRentals.Bookings.Infrastructure.Entities
{
    public class MemberEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

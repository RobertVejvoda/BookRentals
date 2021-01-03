using System;

namespace BookRentals.Core.Infrastructure.Entities
{
    public abstract class AuditableEntity : Entity
    {
        public byte[] Version { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int ModifiedById { get; set; }
    }
}

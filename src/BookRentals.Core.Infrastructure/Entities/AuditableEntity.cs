using System;

namespace BookRentals.Core.Infrastructure.Entities
{
    public abstract class AuditableEntity
    {
        public Guid Id { get; set; }
        public int Ident { get; set; }
        public byte[] Version { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int ModifiedById { get; set; }
    }
}

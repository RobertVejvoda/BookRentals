using System;

namespace BookRentals.Core.Infrastructure.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public int Ident { get; set; }

        public bool IsNew() => Id.Equals(Guid.Empty);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRentals.Core.Domain
{
    public abstract class DomainObject
    {
        private int? requestedHashCode;
        public virtual int Id { get; protected set; }

        private readonly Queue<IDomainEvent> domainEvents = new Queue<IDomainEvent>();

        public void EnqueueDomainEvent(IDomainEvent eventItem)
        {
            domainEvents.Enqueue(eventItem);
        }

        public IReadOnlyCollection<IDomainEvent> DequeueDomainEvents()
        {
            var events = domainEvents.ToList();
            domainEvents.Clear();
            return events;
        }

        public bool IsTransient()
        {
            return Id == default;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is DomainObject))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (GetType() != obj.GetType())
                return false;

            DomainObject item = (DomainObject)obj;

            if (item.IsTransient() || IsTransient())
                return false;
            else
                return item.Id == Id;
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!requestedHashCode.HasValue)
                    requestedHashCode = Id.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

                return requestedHashCode.Value;
            }
            else
                return base.GetHashCode();

        }
        public static bool operator ==(DomainObject left, DomainObject right)
        {
            if (Equals(left, null))
                return Equals(right, null) ? true : false;
            else
                return left.Equals(right);
        }

        public static bool operator !=(DomainObject left, DomainObject right)
        {
            return !(left == right);
        }
    }
}

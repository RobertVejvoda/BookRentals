using System;
using System.Collections.Generic;
using System.Linq;

namespace BookRentals.Core.Domain
{
    public abstract class DomainObject
    {
        private readonly Queue<IDomainEvent> domainEvents = new Queue<IDomainEvent>();

        protected void EnqueueDomainEvents<TDomainEvent>(IEnumerable<TDomainEvent> domainEvents) where TDomainEvent : IDomainEvent
        {
            foreach (var @event in domainEvents)
                EnqueueDomainEvent(@event);
        }

        protected void EnqueueDomainEvent<TDomainEvent>(TDomainEvent eventItem) where TDomainEvent : IDomainEvent
        {
            domainEvents.Enqueue(eventItem);
        }

        public virtual IReadOnlyCollection<IDomainEvent> DequeueDomainEvents()
        {
            var events = domainEvents.ToList();
            domainEvents.Clear();
            return events;
        }
    }

    public abstract class DomainObject<T> : DomainObject
    {
        private int? requestedHashCode;
        public virtual T Id { get; protected set; }

        public virtual bool IsTransient()
        {
            return Id.Equals(default);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is DomainObject<T>))
                return false;

            if (Object.ReferenceEquals(this, obj))
                return true;

            if (GetType() != obj.GetType())
                return false;

            DomainObject<T> item = (DomainObject<T>)obj;

            if (item.IsTransient() || IsTransient())
                return false;

            if (Object.Equals(item.Id, null))
                return (Object.Equals(Id, null)) ? true : false;
            else
                return item.Id.Equals(Id);
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

        public static bool operator ==(DomainObject<T> left, DomainObject<T> right)
        {
            if (Object.Equals(left, null))
                return (Object.Equals(right, null)) ? true : false;
            else
                return left.Equals(right);
        }

        public static bool operator !=(DomainObject<T> left, DomainObject<T> right)
        {
            return !(left == right);
        }
    }
}

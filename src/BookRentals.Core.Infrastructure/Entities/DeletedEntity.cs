using System;

namespace BookRentals.Core.Infrastructure.Entities
{
    public class DeletedEntity
    {
        public Guid Id { get; set; }
        public Guid EntityId { get; set; }
        public string EntityName { get; set; }
        public DateTime DeletedOn { get; set; }
        public int DeletedById { get; set; }
    }
}

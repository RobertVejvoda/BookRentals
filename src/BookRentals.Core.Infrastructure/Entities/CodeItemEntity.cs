using BookRentals.Core.Domain;
using System;

namespace BookRentals.Core.Infrastructure.Entities
{
    public class CodeItemEntity : IAuditableEntity, IVersionable
    {
        public Guid Id { get; set; }
        public int CodeItemId { get; set; }
        public string CodeItemRef { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedById { get; set; }
        public long Version { get; set; }
    }
}
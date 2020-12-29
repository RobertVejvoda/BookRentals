using BookRentals.Core.Infrastructure.Entities;
using System.Collections.Generic;

namespace BookRentals.Engine.Infrastructure.Entities
{
    public class CodeItemEntity : AuditableEntity
    {
        public int CodeItemId { get; set; }
        public string CodeItemRef { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }

        public virtual ICollection<CodeGroupItemEntity> CodeGroupItems { get; set; }
    }
}
using BookRentals.Core.Infrastructure.Entities;
using System.Collections.Generic;

namespace BookRentals.Engine.Infrastructure.Entities
{
    public class CodeGroupEntity : AuditableEntity
    {
        public int CodeGroupId { get; set; }
        public string CodeGroupRef { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }

        public virtual ICollection<CodeGroupItemEntity> CodeGroupItems { get; set; }
    }
}

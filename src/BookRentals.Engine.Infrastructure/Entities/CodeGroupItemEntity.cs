using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRentals.Engine.Infrastructure.Entities
{
    public class CodeGroupItemEntity
    {
        public Guid CodeGroupId { get; set; }
        public Guid CodeItemId { get; set; }
        public int Ident { get; set; }
        public string ConfigurationKey { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsDisabled { get; set; }

        public virtual CodeGroupEntity CodeGroup { get; set; }
        public virtual CodeItemEntity CodeItem { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRentals.Core.Infrastructure.Entities
{
    public class LanguageEntity : AuditableEntity
    {
        public int LanguageId { get; set; }
        public string Caption { get; set; }
        public string CultureCode { get; set; }
    }
}

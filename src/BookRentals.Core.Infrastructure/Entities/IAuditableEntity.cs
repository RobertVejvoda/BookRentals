using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRentals.Core.Infrastructure.Entities
{
    public interface IAuditableEntity
    {
        DateTime CreatedDate { get; set; }
        int CreatedById { get; set; }
    }
}

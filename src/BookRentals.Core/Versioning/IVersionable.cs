using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRentals.Core.Versioning
{
    public interface IVersionable
    {
        byte[] Version { get; }
    }
}

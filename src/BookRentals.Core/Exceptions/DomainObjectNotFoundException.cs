using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BookRentals.Core.Exceptions
{
    [Serializable]
    public class DomainObjectNotFoundException : Exception
    {
        public DomainObjectNotFoundException()
        {
        }

        public DomainObjectNotFoundException(string message) : base(message)
        {
        }

        public DomainObjectNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DomainObjectNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Paginas.Api.Infrastructure.Exceptions
{
    public class PaginasException : Exception
    {
        public PaginasException()
        {
        }

        public PaginasException(string message) : base(message)
        {
        }

        public PaginasException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PaginasException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

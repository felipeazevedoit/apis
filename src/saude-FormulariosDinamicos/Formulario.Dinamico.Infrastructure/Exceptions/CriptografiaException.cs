using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Dinamico.Infrastructure.Exceptions
{
    public class CriptografiaException : Exception
    {
        public CriptografiaException()
        {
        }

        public CriptografiaException(string message) : base(message)
        {
        }

        public CriptografiaException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CriptografiaException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

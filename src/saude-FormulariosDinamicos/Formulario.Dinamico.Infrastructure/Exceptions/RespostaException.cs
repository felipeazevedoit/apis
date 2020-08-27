using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Dinamico.Infrastructure.Exceptions
{
    public class RespostaException : Exception
    {
        public RespostaException()
        {
        }

        public RespostaException(string message) : base(message)
        {
        }

        public RespostaException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RespostaException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

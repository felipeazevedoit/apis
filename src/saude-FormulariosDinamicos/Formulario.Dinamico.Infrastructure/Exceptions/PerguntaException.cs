using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Dinamico.Infrastructure.Exceptions
{
    public class PerguntaException : Exception
    {
        public PerguntaException()
        {
        }

        public PerguntaException(string message) : base(message)
        {
        }

        public PerguntaException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PerguntaException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mensagens.Infrastructure.Exceptions
{
    public class MensagensException : Exception
    {
        public MensagensException()
        {
        }

        public MensagensException(string message) : base(message)
        {
        }

        public MensagensException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MensagensException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

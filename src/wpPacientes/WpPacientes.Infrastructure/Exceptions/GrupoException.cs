using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace WpPacientes.Infrastructure.Exceptions
{
    public class GrupoException : Exception
    {
        public GrupoException()
        {
        }

        public GrupoException(string message) : base(message)
        {
        }

        public GrupoException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GrupoException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

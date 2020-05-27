using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Paginas.Api.Infrastructure.Exceptions
{
    public class PaginaXPacienteException : Exception
    {
        public PaginaXPacienteException()
        {
        }

        public PaginaXPacienteException(string message) : base(message)
        {
        }

        public PaginaXPacienteException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PaginaXPacienteException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

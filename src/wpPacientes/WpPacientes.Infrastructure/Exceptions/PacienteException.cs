using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace WpPacientes.Infrastructure.Exceptions
{
    public class PacienteException : Exception
    {
        public PacienteException()
        {
        }

        public PacienteException(string message) : base(message)
        {
        }

        public PacienteException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PacienteException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

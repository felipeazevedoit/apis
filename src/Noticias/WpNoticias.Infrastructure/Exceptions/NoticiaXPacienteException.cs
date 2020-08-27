using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace WpNoticias.Infrastructure.Exceptions
{
    public class NoticiaXPacienteException : Exception
    {
        public NoticiaXPacienteException()
        {
        }

        public NoticiaXPacienteException(string message) : base(message)
        {
        }

        public NoticiaXPacienteException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoticiaXPacienteException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

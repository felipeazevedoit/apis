using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace WpMedicos.Infrastructure.Exceptions
{
    public class MedicoXPacienteException : Exception
    {
        public MedicoXPacienteException()
        {
        }

        public MedicoXPacienteException(string message) : base(message)
        {
        }

        public MedicoXPacienteException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MedicoXPacienteException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

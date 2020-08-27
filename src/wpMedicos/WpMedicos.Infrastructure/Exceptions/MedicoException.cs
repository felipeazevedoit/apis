using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace WpMedicos.Infrastructure.Exceptions
{
    public class MedicoException : Exception
    {
        public MedicoException()
        {
        }

        public MedicoException(string message) : base(message)
        {
        }

        public MedicoException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MedicoException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace WpPacientes.Infrastructure.Exceptions
{
    public class ConvenioException : Exception
    {
        public ConvenioException()
        {
        }

        public ConvenioException(string message) : base(message)
        {
        }

        public ConvenioException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ConvenioException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace WpNoticias.Infrastructure.Exceptions
{
    public class ComentarioException : Exception
    {
        public ComentarioException()
        {
        }

        public ComentarioException(string message) : base(message)
        {
        }

        public ComentarioException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ComentarioException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

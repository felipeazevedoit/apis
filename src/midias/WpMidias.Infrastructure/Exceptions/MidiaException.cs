using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace WpMidias.Infrastructure.Exceptions
{
    public class MidiaException : Exception
    {
        public MidiaException()
        {
        }

        public MidiaException(string message) : base(message)
        {
        }

        public MidiaException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MidiaException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SaudeComVoce.Exceptions
{
    public class NoticiaException : Exception
    {
        public NoticiaException()
        {
        }

        public NoticiaException(string message) : base(message)
        {
        }

        public NoticiaException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoticiaException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
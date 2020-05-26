using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SaudeComVc_Home.Exceptions
{
    public class PerguntasException : Exception
    {
        public PerguntasException()
        {
        }

        public PerguntasException(string message) : base(message)
        {
        }

        public PerguntasException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PerguntasException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
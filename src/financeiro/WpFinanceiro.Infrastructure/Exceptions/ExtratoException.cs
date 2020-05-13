using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace WpFinanceiro.Infrastructure.Exceptions
{
    public class ExtratoException : Exception
    {
        public ExtratoException()
        {
        }

        public ExtratoException(string message) : base(message)
        {
        }

        public ExtratoException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ExtratoException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

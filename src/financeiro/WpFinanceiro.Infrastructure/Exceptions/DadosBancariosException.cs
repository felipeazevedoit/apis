using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace WpFinanceiro.Infrastructure.Exceptions
{
    public class DadosBancariosException : Exception
    {
        public DadosBancariosException()
        {
        }

        public DadosBancariosException(string message) : base(message)
        {
        }

        public DadosBancariosException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DadosBancariosException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

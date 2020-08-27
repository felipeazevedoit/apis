using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace WpNoticias.Infrastructure.Exceptions
{
    public class CategoriaException : Exception
    {
        public CategoriaException()
        {
        }

        public CategoriaException(string message) : base(message)
        {
        }

        public CategoriaException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CategoriaException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

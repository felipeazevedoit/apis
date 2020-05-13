using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace WpMidias.Domains.Helpers.Exceptions
{
    public class FileSystemException : Exception
    {
        public FileSystemException()
        {
        }

        public FileSystemException(string message) : base(message)
        {
        }

        public FileSystemException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FileSystemException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

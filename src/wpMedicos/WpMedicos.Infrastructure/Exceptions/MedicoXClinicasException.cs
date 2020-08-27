using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace WpMedicos.Infrastructure.Exceptions
{
    public class MedicoXClinicasException : Exception
    {
        public MedicoXClinicasException()
        {
        }

        public MedicoXClinicasException(string message) : base(message)
        {
        }

        public MedicoXClinicasException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MedicoXClinicasException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace WpMedicos.Infrastructure.Exceptions
{
    public class MedicosXEspecialidadesException : Exception
    {
        public MedicosXEspecialidadesException()
        {
        }

        public MedicosXEspecialidadesException(string message) : base(message)
        {
        }

        public MedicosXEspecialidadesException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MedicosXEspecialidadesException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

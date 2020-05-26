﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SaudeComVc_Home.Exceptions
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
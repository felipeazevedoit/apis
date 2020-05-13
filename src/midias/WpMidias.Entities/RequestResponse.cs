using System;
using System.Collections.Generic;
using System.Text;

namespace WpMidias.Entities
{
    public class RequestResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
    }
}

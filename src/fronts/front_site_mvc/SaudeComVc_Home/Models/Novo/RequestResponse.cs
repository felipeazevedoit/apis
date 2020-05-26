using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaudeComVc_Home.Models.Novo
{
    public class RequestResponse <T> where T : class
    {
        public bool Success { get; set; }
        public T Data { get; set; }
    }
}
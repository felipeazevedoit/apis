using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaudeComVc_Home.Models.Novo.Request
{
    public class LoginAuth
    {
        public string User { get; set; }
        public string Password { get; set; }
        public int IdCliente { get; set; }
    }
}
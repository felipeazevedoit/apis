using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaudeComVc_Home.Helpers
{
    public class NoticiaObject
    {
        public int idCliente { get; set; }

        public int codigoExterno { get; set; }

        public IEnumerable<string> tags { get; set; }

        public string NoticiaTags { get; set; }
    }
}
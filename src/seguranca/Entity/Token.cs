using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
   public class Token : Base
    {
        public string GuidSec { get; set; }
        public string Valido { get; set; }
        public string IP { get; set; }
        public int idUsuario { get; set; }
        public DateTime DataExpiracao { get; set; }
        public int idAux { get; set; }
        public string UrlCliente { get; set; }
    }
}

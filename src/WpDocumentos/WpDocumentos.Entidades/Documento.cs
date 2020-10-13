using System;
using System.Collections.Generic;
using System.Text;

namespace WpDocumentos.Entidades
{
    public class Documento : Base
    {
        public int tipo { get; set; }
        public Propriedades propriedades { get; set; }
        public Layout layout { get; set; }
        public Tradutor tradutor { get; set; }
        public bool Gravar { get; set; }
    }
}

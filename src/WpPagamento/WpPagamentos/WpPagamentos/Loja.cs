using System;
using System.Collections.Generic;
using System.Text;

namespace WpPagamentos.Entidade
{
    public class Loja : Base
    {
        public int idPedido { get; set; }
        public Propriedades propiedades { get; set; }
        public MeioPagamento meioPagamento {get;set;}

    }
}

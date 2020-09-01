using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity
{
    public class Carrinho : Base
    {
        public List<Produto> Produtos { get; set; }   
        public Transportadora Transportadora { get; set; }
        public List<Propriedades> Propiedades { get; set; }
        public List<string> Menssagens { get; set; }
        public Decimal Total { get; set; }
        public Decimal SubTotal { get; set; }
    }
}

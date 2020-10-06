using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace WpCotacao.Entidades
{
    public class Produto : Base
    {
        public double Valor { get; set; }
        public int Parcela { get; set; }
        public double subTotal { get; set; }
        public List<Cobertura> Coberturas { get; set; }
    }
}

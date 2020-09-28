using System;
using System.Collections.Generic;
using System.Text;

namespace WpCotacao.Entidades
{
    public class Proposta : Base
    {
        public Usuario usuario { get; set; }
        public Produto produto { get; set; }
        public string meioPagamento { get; set; }
        public double Total { get; set; }
        public Beneficiario beneficiario { get; set; }
    }
}

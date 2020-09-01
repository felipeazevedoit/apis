using System;
using System.Collections.Generic;
using System.Text;
using WpPagamentos.Entidade;

namespace WpPagamentos.Servico.Model
{
    public class Configuracao : Base
    {
        public string Chave { get; set; }
        public string Valor { get; set; }
    }
}

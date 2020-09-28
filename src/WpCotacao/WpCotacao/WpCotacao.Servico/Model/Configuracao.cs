using System;
using System.Collections.Generic;
using System.Text;
using WpCotacao.Entidades;

namespace WpCotacao.Servico.Model
{
    public class Configuracao : Base
    {
        public string Chave { get; set; }
        public string Valor { get; set; }
    }
}

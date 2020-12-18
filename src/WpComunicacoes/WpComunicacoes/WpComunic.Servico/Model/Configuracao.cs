using System;
using System.Collections.Generic;
using System.Text;
using WpComunicacoes.Entidades;

namespace WpComunic.Servico.Model
{
    public class Configuracao : Base
    {
        public string Chave
        {
            get; set;
        }
        public string Valor
        {
            get; set;
        }
    }
}

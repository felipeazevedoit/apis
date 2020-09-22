using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WpPagamentos.Entidade
{


    public class MeioPagamento : Base
    {

        [NotMapped]
        public Object Configuracao { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WpPagamentos.Entidade
{

    [NotMapped]
    public class MeioPagamento : Base
    {

        [NotMapped]
        public Object Configuracao { get; set; }
    }
}
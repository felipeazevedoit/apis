using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WpPagamentos.Entidade
{
    [NotMapped]
    public class Captura
    {
        public object DadosCaptura { get; set; }
    }
}

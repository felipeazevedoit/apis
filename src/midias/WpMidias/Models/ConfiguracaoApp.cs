using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WpMidias.Models
{
    public class ConfiguracaoApp
    {
        /// <summary>
        /// Id Identity gerado pelo BD
        /// </summary>
        public int ConfigSistemaId { get; set; }

        /// <summary>
        /// Chave unica de identificação do registro
        /// </summary>
        public string Chave { get; set; }

        /// <summary>
        /// Valor da chave
        /// </summary>
        public string Valor { get; set; }
       
    }
}

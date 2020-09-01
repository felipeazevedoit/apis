using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WpPagamentos.API.Model
{
    public class ParametroViewModel : BaseViewModel
    {
        public string Tipo { get; set; }
        public int idAcao { get; set; }
        public int Ordem { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPixSeguranca.Model
{
    public class AcaoViewModel : BaseViewModel
    {
        public int TipoAcao { get; set; }
        public string Tipo { get; set; }
        public string Caminho { get; set; }
        public int idMotorAux { get; set; }
        public List<ParametroViewModel> Parametro { get; set; }
    }
}

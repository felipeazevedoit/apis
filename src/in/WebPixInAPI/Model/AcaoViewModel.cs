using System.Collections.Generic;

namespace WebPixInAPI.Model
{
    public class AcaoViewModel : BaseViewModel
    {
        public int TipoAcao { get; set; }
        public string Caminho { get; set; }
        public int idMotorAux { get; set; }
        public List<ParametroViewModel> Parametro { get; set; }
    }
}
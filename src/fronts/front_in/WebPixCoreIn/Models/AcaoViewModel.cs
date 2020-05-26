

using System.ComponentModel.DataAnnotations.Schema;

namespace WebPixCoreIn.Models
{
    public class AcaoViewModel : BaseViewModel
    {
        public int idTipoAcao { get; set; }
        public string Caminho { get; set; }
        public int idMotorAux { get; set; }
        public string MotorAuxiliar { get; set; }
        public string TipoAcao { get; set; }
    }
}
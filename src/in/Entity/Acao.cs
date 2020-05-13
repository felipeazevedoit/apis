using System.Collections.Generic;

namespace Entity
{
    public class Acao : Base
    {
        public int idTipoAcao { get; set; }
        public string Caminho { get; set; }
        public int idMotorAux { get; set; }
    }
}
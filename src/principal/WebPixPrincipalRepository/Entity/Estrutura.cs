using System;
using System.Collections.Generic;
using System.Text;

namespace WebPixPrincipalRepository.Entity
{
    public class Estrutura : Base
    {
        public int Tipo { get; set; }
        public int idTipoAcao { get; set; }
        public int idPai { get; set; }
        public string Principal { get; set; }
        public string Imagem { get; set; }
        public string ImagemMenu { get; set; }
        public string LinkManual { get; set; }
        public string UrlManual { get; set; }
        public int Ordem { get; set; }
        public int IdMotorAux { get; set; }
    }
}

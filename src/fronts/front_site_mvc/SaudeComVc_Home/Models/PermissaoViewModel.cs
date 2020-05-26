using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaudeComVoce.Models
{
    public class PermissaoViewModel : BaseModel
    {
        public int IdAux { get; set; }
        public string idTipoAcao { get; set; }
        public string VAdmin { get; set; }
    }
}
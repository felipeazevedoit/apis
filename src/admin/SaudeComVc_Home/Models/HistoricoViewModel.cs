using Newtonsoft.Json;
using SaudeComVoce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaudeComVc_Home.Models
{
    public class HistoricoViewModel : BaseModel
    {
        public string Instituicao { get; set; }
        public DateTime InicioCurso { get; set; }
        public DateTime Finalcurso { get; set; }
        public int? ProfissionalId { get; set; }
        public int CodigoExterno { get; set; }

        [JsonIgnore]
        public string MedicoNome { get; set; }
    }
}
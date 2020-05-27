using Newtonsoft.Json;
using SaudeComVoce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaudeComVc_Home.Models
{
    public class PerguntaViewModel : BaseModel
    {
        public int PerguntaPaiId { get; set; }
        public int TipoRespostaId { get; set; }
        public TipoRespostaViewModel TipoResposta { get; set; }
        public ICollection<RespostaViewModel> Respostas { get; set; }

        [JsonIgnore]
        public int CodigoExterno { get; set; }

        [JsonIgnore]
        public string Foto { get; set; }
        [JsonIgnore]
        public string Extensao { get; set; }

        [JsonIgnore]
        public IEnumerable<PerguntaViewModel> PerguntasFilho { get; set; }
    }
}
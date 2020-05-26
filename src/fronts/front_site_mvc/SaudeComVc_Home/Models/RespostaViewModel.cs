using SaudeComVoce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaudeComVc_Home.Models
{
    public class RespostaViewModel : BaseModel
    {
        public int CodigoExterno { get; set; }
        public int PerguntaId { get; set; }
        public PerguntaViewModel Pergunta { get; set; }
        public string EntidadeNome { get; set; }
    }
}
using SaudeComVoce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaudeComVc_Home.Models
{
    public class FichaFeed
    {
        public IEnumerable<PerguntaViewModel> Perguntas { get; set; }

        public IEnumerable<NoticiaViewModel> Noticias { get; set; }

        public FichaFeed(IEnumerable<PerguntaViewModel> perguntas)
        {
            Perguntas = perguntas;
        }
    }
}
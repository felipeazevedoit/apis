using SaudeComVoce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaudeComVc_Home.Models
{
    public class HomeViewModel
    {
        public HomeViewModel(IEnumerable<MedicoViewModel> medicos, IEnumerable<PerguntaViewModel> perguntas)
        {
            Medicos = medicos;
            Perguntas = perguntas;
        }

        public IEnumerable<MedicoViewModel> Medicos { get; set; }
        public IEnumerable<PerguntaViewModel> Perguntas { get; set; }
    }
}
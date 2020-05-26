using SaudeComVoce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaudeComVc_Home.Models
{
    public class FeedViewModel
    {
        public IEnumerable<NotificacaoViewModel> Notificacoes { get; set; }
        public IEnumerable<NoticiaViewModel> Noticias { get; set; }

        public IEnumerable<MedicoViewModel> Medicos { get; set; }

        public IEnumerable<PacienteViewModel> Pacientes { get; set; }

        public FeedViewModel()
        {

        }

        public FeedViewModel(IEnumerable<NotificacaoViewModel> notificacoes, IEnumerable<NoticiaViewModel> noticias)
        {
            Notificacoes = notificacoes;
            Noticias = noticias;
        }
    }
}
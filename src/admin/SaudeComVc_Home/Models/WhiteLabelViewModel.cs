using SaudeComVoce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaudeComVc_Home.Models
{
    public class WhiteLabelViewModel
    {
        public int IdMedico { get; set; }
        public string Nome { get; set; }
        public PaginaViewModel Pagina { get; set; }
        public IEnumerable<MidiaViewModel> Galeria { get; set; }
        public IEnumerable<NoticiaViewModel> Noticias { get; set; }
        public IEnumerable<HistoricoViewModel> Historico { get; set; }

        public WhiteLabelViewModel(int idMedico, PaginaViewModel pagina, IEnumerable<MidiaViewModel> galeria, IEnumerable<NoticiaViewModel> noticias, IEnumerable<HistoricoViewModel> historico)
        {
            IdMedico = idMedico;
            Nome = pagina.Nome;
            Pagina = pagina;
            Galeria = galeria;
            Noticias = noticias;
            Historico = historico;
        }

        public WhiteLabelViewModel()
        {

        }
    }
}
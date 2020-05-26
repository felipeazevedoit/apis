using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaudeComVc_Home.Models
{
    public class PaginaXPacienteViewModel
    {
        public int ID { get; set; }
        public int PaginaId { get; set; }
        //public Pagina Pagina { get; set; }
        public int PacienteId { get; set; }
        public DateTime DataVisualizacao { get; set; }
    }
}
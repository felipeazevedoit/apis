using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaudeComVc_Home.Models
{
    public class NoticiaXPacienteViewModel
    {
        public int ID { get; set; }
        public int PacienteId { get; set; }
        public int NoticiaId { get; set; }
        public DateTime DataVisualizacao { get; set; }
    }
}
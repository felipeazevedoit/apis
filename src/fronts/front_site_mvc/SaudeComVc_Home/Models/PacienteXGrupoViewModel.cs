using SaudeComVoce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaudeComVc_Home.Models
{
    public class PacienteXGrupoViewModel
    {
        public int ID { get; set; }
        public int GrupoId { get; set; }
        public int PacienteId { get; set; }

        public PacienteXGrupoViewModel()
        {

        }
    }
}
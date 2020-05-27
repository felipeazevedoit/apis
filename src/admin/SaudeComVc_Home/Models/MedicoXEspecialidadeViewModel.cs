using SaudeComVoce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaudeComVc_Home.Models
{
    public class MedicoXEspecialidadeViewModel
    {
        public int ID { get; set; }

        public int MedicoId { get; set; }
        public MedicoViewModel Medico { get; set; }
        public int EspecialidadeId { get; set; }
        public EspecialidadeViewModel Especialidade { get; set; }
    }
}
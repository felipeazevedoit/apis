using SaudeComVoce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaudeComVc_Home.Models
{
    public class MedicoXPaciente : BaseModel
    {
        public int IdPaciente { get; set; }
        public int MedicoId { get; set; }
        public MedicoViewModel Medico { get; set; }

    }
}
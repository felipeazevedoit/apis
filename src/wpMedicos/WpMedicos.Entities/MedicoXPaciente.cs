using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WpMedicos.Entities
{
    public class MedicoXPaciente : Base
    {
        public int IdPaciente { get; set; }
        public int MedicoId { get; set; }
        public Medico Medico { get; set; }

    }

}

    

using System;
using System.Collections.Generic;
using System.Text;

namespace WpMedicos.Entities
{
    public class Telefone : Base
    {
        public string Numero { get; set; }
        public int MedicoId { get; set; }
        public Medico Medico { get; set; }
    }
}

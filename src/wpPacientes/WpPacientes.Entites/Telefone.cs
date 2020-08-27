using System;
using System.Collections.Generic;
using System.Text;

namespace WpPacientes.Entities
{
    public class Telefone:Base
    {
        public string Numero { get; set; }
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }
    }
}

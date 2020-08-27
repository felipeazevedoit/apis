using System;
using System.Collections.Generic;
using System.Text;

namespace WpPacientes.Entities
{
    public class PacientesXGrupos
    {
        public int ID { get; set; }
        public int GrupoId { get; set; }
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }
        public Grupo Grupo { get; set; }
    }
}

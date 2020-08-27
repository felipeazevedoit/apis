using System;
using System.Collections.Generic;
using System.Text;

namespace WpMedicos.Entities
{
    public class MedicoXClinicas
    {
        public int ID { get; set; }
        public int MedicoId { get; set; }
        public Medico Medico { get; set; }
        public int ClinicaId { get; set; }

        public MedicoXClinicas()
        {
        }
        public MedicoXClinicas(Medico medico)
        {
            MedicoId = medico.ID;
            Medico = medico;
        }
        public MedicoXClinicas(int id, int medicoId, int clinicaId)
        {
            ID = id;
            MedicoId = medicoId;
            ClinicaId = clinicaId;
        }
    }
}

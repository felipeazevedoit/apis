using System;
using System.Collections.Generic;
using System.Text;

namespace WpMedicos.Entities
{
    public class MedicoXEspecialidade
    {
        public int ID { get; set; }

        public int MedicoId { get; set; }
        public Medico Medico { get; set; }
        public int EspecialidadeId { get; set; }
        public Especialidade Especialidade { get; set; }


        public MedicoXEspecialidade()
        {

        }

        public MedicoXEspecialidade(Medico medico, Especialidade especialidade)
        {
            MedicoId = medico.ID;
            Medico = medico;
            EspecialidadeId = especialidade.ID;
            Especialidade = especialidade;
        }

        public MedicoXEspecialidade(int id, int medicoId, int especialidadeId)
        {
            ID = id;
            MedicoId = medicoId;
            EspecialidadeId = especialidadeId;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WpMedicos.Entities
{
    public class Medico : Base
    {
        public int IdUsuario { get; set; }
        public int CodigoExterno { get; set; }
        public string CRM { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public string UF_CRM { get; set; }
        public string ClinicaNome { get; set; }
        public Endereco Endereco { get; set; }
        public Telefone Telefone { get; set; }
        public int ClinicaId { get; set; }

        [NotMapped]
        public int EspecialidadeId { get; set; }
        [NotMapped]
        public Especialidade Especialidade { get; set; }

        [NotMapped]
        public List<int> idsClinicas { get; set; }
    }
}

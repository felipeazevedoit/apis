using System;

namespace WpPacientes.Entities
{
    public class Paciente : Base
    {
        public int CodigoExterno { get; set; }
        public string Sexo { get; set; }
        public DateTime DataNascimento { get; set; }
        public decimal Peso { get; set; }
        public decimal Altura { get; set; }
        public string SobreNome { get; set; }
        public string CPF { get; set; }
        public int ConvenioId { get; set; }
        public Endereco Endereco { get; set; }
        public Telefone Telefone { get; set; }   
    }
}

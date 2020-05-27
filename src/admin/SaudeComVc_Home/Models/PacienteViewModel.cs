using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaudeComVoce.Models
{
    public class PacienteViewModel : BaseModel
    {
        public int CodigoExterno { get; set; }
        public string Sexo { get; set; }
        public DateTime DataNascimento { get; set; }
        public decimal Peso { get; set; }
        public decimal Altura { get; set; }
        public string SobreNome { get; set; }
        public string CPF { get; set; }
        public int ConvenioId { get; set; }

        public PacienteEnderecoViewModel Endereco { get; set; }
        public TelefonePacienteViewModel Telefone { get; set; }

        [JsonIgnore]
        public string Login { get; set; }
        [JsonIgnore]
        public string Senha { get; set; }
        [JsonIgnore]
        public string SenhaConfirmada { get; set; }
        [JsonIgnore]
        public int? IdPerfil { get; set; }
    }
}
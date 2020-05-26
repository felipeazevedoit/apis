using Newtonsoft.Json;
using SaudeComVc_Home.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaudeComVoce.Models
{
    public class MedicoViewModel : BaseModel
    {
        public int IdUsuario { get; set; }
        public int? CodigoExterno { get; set; }
        public int? EspecialidadeId { get; set; }
        public string CRM { get; set; }
        public string UF_CRM { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public MedicoEnderecoViewModel Endereco { get; set; }

        [JsonIgnore]
        public string Clinica { get; set; }
        [JsonIgnore]
        public string Especialidade { get; set; }
        [JsonIgnore]
        public string Login { get; set; }
        [JsonIgnore]
        public string Senha { get; set; }
        [JsonIgnore]
        public string SenhaConfirmada { get; set; }
        [JsonIgnore]
        public int? IdPerfil { get; set; }
        [JsonIgnore]
        public string Foto { get; set; }
        [JsonIgnore]
        public string Extensao { get; set; }
        [JsonIgnore]
        public string DescricaoWhiteLabel{ get; set; }
    }
}
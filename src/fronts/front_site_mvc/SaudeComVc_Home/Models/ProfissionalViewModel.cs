using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaudeComVoce.Models
{
    public class ProfissionalViewModel : BaseModel
    {
        public string Email { get; set; }
        //public ProfissionalEnderecoViewModel Endereco { get; set; }
        //public TelefoneProfissionalViewModel Telefone { get; set; }
        public IList<object> Formacoes { get; set; }
        public int IdUsuario { get; set; }
        public DateTime DataNascimento { get; set; }
        public decimal Avaliacao { get; set; }
        public object Categoria { get; set; }
        public int? CategoriaId { get; set; }
        public string CPF { get; set; }
        public string RegistroProfissional { get; set; }
        public string UFRegistro { get; set; }

        [JsonIgnore]
        public string AreaAtuacao { get; set; }

        [JsonIgnore]
        public string SenhaConfirmada { get; set; }

        [JsonIgnore]
        public string Login { get; set; }

        [JsonIgnore]
        public string Senha { get; set; }
        [JsonIgnore]
        public int? IdPerfil { get; set; }

        public ProfissionalViewModel()
        {
        }
    }
}
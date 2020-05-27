using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace SaudeComVoce.Models
{
    public class UsuarioViewModel : BaseModel
    {
        public string Login { get; set; }
        public string SobreNome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int IdEmpresa { get; set; }
        public string Perfil { get; set; }
        //public string Empresa { get; set; }
        public string Avatar { get; set; }
        public string VAdmin { get; set; }
        public string ProfileAvatar { get; set; }
        public string AvatarExtension { get; set; }
        public bool? Termo { get; set; }

        [JsonIgnore]
        public UsuarioXPerfilViewModel UsuarioXPerfil { get; set; }

        [JsonIgnore]
        public string SenhaConfirmada { get; set; }
        [JsonIgnore]
        public string Altura { get; set; }
        [JsonIgnore]
        public string Peso { get; set; }
        [JsonIgnore]
        public string CPF { get; set; }
        [JsonIgnore]
        public string Sexo { get; set; }
        [JsonIgnore]
        public string Convenio { get; set; }
        [JsonIgnore]
        public string Telefone { get; set; }

        [JsonIgnore]
        public string Rua { get; set; }
        [JsonIgnore]
        public string Bairro { get; set; }
        [JsonIgnore]
        public string Cidade { get; set; }
        [JsonIgnore]
        public string Estado { get; set; }
        [JsonIgnore]
        public string Uf { get; set; }
        [JsonIgnore]
        public string Cep { get; set; }
        [JsonIgnore]
        public string Desc { get; set; }
        [JsonIgnore]
        public int Numero { get; set; }
        [JsonIgnore]
        public int IdTel { get; set; }
        [JsonIgnore]
        public int IdEnd { get; set; }

        public UsuarioViewModel()
        {

        }
    }
}
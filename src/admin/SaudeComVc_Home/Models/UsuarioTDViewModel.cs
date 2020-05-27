using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaudeComVc_Home.Models
{
    public class UsuarioTDViewModel
    {
        public UsuarioTDViewModel(string email, string sessao, DateTime criadoEm, string nome, string tipoSessao, string deviceId, string password)
        {
            Email = email;
            Sessao = sessao;
            CriadoEm = criadoEm;
            Nome = nome;
            TipoSessao = tipoSessao;
            DeviceId = deviceId;
            Password = password;
        }
        public UsuarioTDViewModel()
        {

        }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("sessao")]
        public string Sessao { get; set; }
        [JsonProperty("criado_em")]
        public DateTime CriadoEm { get; set; }
        [JsonProperty("nome")]
        public string Nome { get; set; }
        [JsonProperty("tipo_sessao")]
        public string TipoSessao { get; set; }
        [JsonProperty("device_id")]
        public string DeviceId { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
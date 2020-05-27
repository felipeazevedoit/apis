using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaudeComVc_Home.Models
{
    public class TelDoctorViewModel
    {
        public TelDoctorViewModel(string grantType, string username, string password, string clientId, string clienteSecret)
        {
            GrantType = grantType;
            Username = username;
            Password = password;
            ClientId = clientId;
            ClienteSecret = clienteSecret;
        }
        public TelDoctorViewModel()
        {

        }
        [JsonProperty("grant_type")]
        public string GrantType { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("client_id")]
        public string ClientId { get; set; }
        [JsonProperty("client_secret")]
        public string ClienteSecret { get; set; }

    }
}
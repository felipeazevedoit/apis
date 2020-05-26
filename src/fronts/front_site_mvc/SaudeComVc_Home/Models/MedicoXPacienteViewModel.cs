using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaudeComVc_Home.Models
{
    public class MedicoXPacienteViewModel
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public int IdPaciente { get; set; }
        public int MedicoId { get; set; }
        public int? UsuarioCriacao { get; set; }

        [JsonIgnore]
        public bool? Termo { get; set; }
    }
}
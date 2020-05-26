using Newtonsoft.Json;
using SaudeComVoce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaudeComVc_Home.Models
{
    public class PesquisaViewModel : BaseModel
    {
        public string Extensao { get; set; }
        public string ImagemB64 { get; set; }

        [JsonIgnore]
        public string Url { get; set; }
    }
}
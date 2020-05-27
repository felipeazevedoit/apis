using SaudeComVoce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaudeComVc_Home.Models
{
    public class ConviteViewModel : BaseModel
    {
        public int CodigoExterno { get; set; }
        public string EmailConvidado { get; set; }
        public string Token { get; set; }
        public int IdConvidado { get; set; }
        public bool Visualizado { get; set; }
        public string NomeConvidado { get; set; }
        public string EmailMedico { get; set; }
    }
}
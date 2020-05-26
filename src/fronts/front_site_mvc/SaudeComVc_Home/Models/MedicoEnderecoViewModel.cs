using SaudeComVoce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaudeComVc_Home.Models
{
    public class MedicoEnderecoViewModel : BaseModel
    {
        public string CEP { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Local { get; set; }
        public int NumeroLocal { get; set; }
        public string Complemento { get; set; }
        public int IdUsuario { get; set; }
        public string Uf { get; set; }
        public int MedicoId { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaudeComVc_Home.Models.Novo.Response
{
    public class UsuarioResponse
    {
        public string NmLogin { get; set; }
        public string Email { get; set; }
        public string NomeCompleto { get; set; }
        public string IdExterno { get; set; }
        public virtual int PerfilId { get; set; }
        public string VAdmin { get; set; }
        public string Avatar { get; set; }
        public int IdEmpresa { get; set; }
        public string AvatarExtension { get; set; }
    }
}
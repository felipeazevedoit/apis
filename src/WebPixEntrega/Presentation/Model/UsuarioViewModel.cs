using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPixPrincipalAPI.Model
{
    public class UsuarioViewModel : BaseViewModel
    {
        public string Login { get; set; }
        public string SobreNome { get; set; }
        public string Email { get; set; }
        public int PerfilUsuario { get; set; }
        public string Senha { get; set; }
        public string VAdmin { get; set; }
    }
}

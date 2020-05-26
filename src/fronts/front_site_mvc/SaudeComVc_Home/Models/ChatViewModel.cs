using SaudeComVoce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaudeComVc_Home.Models
{
    public class ChatViewModel
    {
        public IEnumerable<UsuarioViewModel> Usuarios { get; set; }

        public ChatViewModel()
        {

        }

        public ChatViewModel(IEnumerable<UsuarioViewModel> usuarios)
        {
            Usuarios = usuarios;
        }
    }
}
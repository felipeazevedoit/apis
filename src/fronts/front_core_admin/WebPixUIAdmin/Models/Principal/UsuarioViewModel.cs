﻿namespace WebPixUIAdmin.Models
{
    public class UsuarioViewModel : BaseModel
    {
        public string Login { get; set; }
        public string SobreNome { get; set; }
        public string Email { get; set; }
        public int PerfilUsuario { get; set; }
        public string Senha { get; set; }
        public string VAdmin { get; set; }
    }
}
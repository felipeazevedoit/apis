using System.ComponentModel.DataAnnotations.Schema;

namespace WebPixPrincipalRepository.Entity
{
    public class Usuario : Base
    {
        public string Login { get; set; }
        public string SobreNome { get; set; }
        public string Email { get; set; }
        [NotMapped]
        public string Senha { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string VAdmin { get; set; }
        public string Avatar { get; set; }
        public int IdEmpresa { get; set; }
        public bool? Termo { get; set; }

        [NotMapped]
        public byte[] ProfileAvatar { get; set; }
        [NotMapped]
        public string AvatarExtension { get; set; }
        [NotMapped]
        public bool EnviarEmail { get; set; }
        [NotMapped]
        public string Origem { get; set; }
        [NotMapped]
        public int Tipo { get; set; }

    }
}

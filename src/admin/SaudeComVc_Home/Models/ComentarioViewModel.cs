using Newtonsoft.Json;

namespace SaudeComVoce.Models
{
    public class ComentarioViewModel : BaseModel
    {
        public NoticiaViewModel Noticia { get; set; }
        public int NoticiaId { get; set; }
        public string profileAvatar { get; set; }
        public string AvatarExtension { get; set; }


        [JsonIgnore]
        public MidiaViewModel Midia { get; set; }

        [JsonIgnore]
        public UsuarioViewModel User { get; set; }

        
        

    }
}
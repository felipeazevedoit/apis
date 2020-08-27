using Newtonsoft.Json;

namespace SaudeComVoce.Models
{
    public class PerfilViewModel : BaseModel
    {
        public string idPermissao { get; set; }
        public string VAdmin { get; set; }

        [JsonIgnore]
        public bool Selected { get; set; }
    }
}

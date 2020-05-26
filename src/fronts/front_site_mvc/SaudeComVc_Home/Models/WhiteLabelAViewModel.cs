using Newtonsoft.Json;
using SaudeComVoce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaudeComVc_Home.Models
{
    public class WhiteLabelAViewModel: BaseModel
    {
        public int CodigoExterno { get; set; }
        public byte[] Banner { get; set; }
        public string Apresentacao { get; set; }
        public string FabebookLink { get; set; }
        public string TwitterLink { get; set; }
        public string IntagramLink { get; set; }

        [JsonIgnore]
        public string Logo { get; set; }
        [JsonIgnore]
        public string Extensao { get; set; }
        [JsonIgnore]
        public IEnumerable<MidiaViewModel> Galeria { get; set; }
        [JsonIgnore]
        public IEnumerable<NoticiaViewModel> Noticias { get; set; }
        [JsonIgnore]
        public MedicoViewModel Medico { get; set; }
        [JsonIgnore]
        public MedicoXPaciente Vinculo { get; set; }
        [JsonIgnore]
        public int IdMedico { get; set; }
        public WhiteLabelAViewModel(int idMedico, IEnumerable<MidiaViewModel> galeria, IEnumerable<NoticiaViewModel> noticias, MedicoViewModel medico)
        {
            IdMedico = idMedico;
            //Nome = pagina.Nome;
            Galeria = galeria;
            Noticias = noticias;
            Medico = medico;
        }

        public WhiteLabelAViewModel()
        {

        }
    }
}
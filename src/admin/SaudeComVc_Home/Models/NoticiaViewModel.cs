using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaudeComVoce.Models
{
    public class NoticiaViewModel : BaseModel
    {
        [JsonIgnore]
        public HttpPostedFileBase Thumbnail { get; set; }
        public string Conteudo { get; set; }
        public IList<ComentarioViewModel> Comentarios { get; set; }
        public bool Privado { get; set; }
        public int Tipo { get; set; }
        public int CodigoExterno { get; set; }
        public int CategoriaId { get; set; }
        public string Tags { get; set; }

        [JsonIgnore]
        public string Tamanho { get; set; }

        public MidiaViewModel Midia { get; set; }
    }
}
using Newtonsoft.Json;
using SaudeComVc_Home.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SaudeComVoce.Models
{
    public class NoticiaViewModel : BaseModel
    {
        //[JsonIgnore]
        //public HttpPostedFileBase Thumbnail { get; set; }
        //public string Conteudo { get; set; }
        //public IList<ComentarioViewModel> Comentarios { get; set; }
        //public bool Privado { get; set; }
        //public int Tipo { get; set; }
        //public int CodigoExterno { get; set; }
        //public int CategoriaId { get; set; }
        //public string Tags { get; set; }

        //[JsonIgnore]
        //public string Tamanho { get; set; }

        public MidiaViewModel Midia { get; set; }
        [JsonIgnore]
        public HttpPostedFileBase Thumbnail { get; set; }
        public string Conteudo { get; set; }
        public IList<ComentarioViewModel> Comentarios { get; set; }
        public bool Privado { get; set; }
        public int Tipo { get; set; }
        public int CodigoExterno { get; set; }
        [JsonIgnore]
        public string Categoria { get; set; }
        public string Grupo { get; set; }
        public int CategoriaId { get; set; }
        public int? GrupoId { get; set; }
        public string Imagem { get; set; }
        [JsonIgnore]
        public string Tamanho { get; set; }
        public string Tags { get; set; }
        //[JsonIgnore]
        //public MidiaViewModel Midia { get; set; }
        [JsonIgnore]
        public string TipoNoticia { get; set; }
        [JsonIgnore]
        public string Autor { get; set; }
        [JsonIgnore]
        public string Foto { get; set; }
        [JsonIgnore]
        public string FotoExtension { get; set; }

        [JsonIgnore]
        public MedicoViewModel Medico { get; set; }

        //public MidiaViewModel Midia { get; set; }
        public static implicit operator Task<object>(NoticiaViewModel v)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PerguntaViewModel> Perguntas { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace WpNoticias.Entities
{
    public class Noticia : Base
    {
        public bool Privado { get; set; }
        public string Conteudo { get; set; }
        public int Tipo { get; set; }
        public IList<Comentario> Comentarios { get; set; }
        public string Tags { get; set; }
        public int CodigoExterno { get; set; }
        public int CategoriaId { get; set; }
        public int? GrupoId { get; set; }
        public Categoria Categoria { get; set; }
    }
}

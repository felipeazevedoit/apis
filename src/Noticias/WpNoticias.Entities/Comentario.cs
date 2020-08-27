using System;
using System.Collections.Generic;
using System.Text;

namespace WpNoticias.Entities
{
    public class Comentario : Base
    {
        public int NoticiaId { get; set; }
        public Noticia Noticia { get; set; }
    }
}

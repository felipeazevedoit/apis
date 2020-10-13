using System.ComponentModel.DataAnnotations.Schema;

namespace WpDocumentos.Entidades
{
    public class Propriedades : Base
    {
        [NotMapped]
        public object paramentros { get; set; }
    }
}
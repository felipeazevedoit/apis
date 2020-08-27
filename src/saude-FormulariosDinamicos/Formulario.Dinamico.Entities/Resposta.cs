using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Formulario.Dinamico.Entities
{
    public class Resposta : Base
    {
        public int CodigoExterno { get; set; }
        public int PerguntaId { get; set; }
        [JsonIgnore]
        public Pergunta Pergunta { get; set; }

        [NotMapped]
        public string EntidadeNome { get; set; }
    }
}

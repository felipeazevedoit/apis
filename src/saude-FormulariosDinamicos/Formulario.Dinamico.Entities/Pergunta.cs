using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Dinamico.Entities
{
    public class Pergunta : Base
    {
        public int PerguntaPaiId { get; set; }
        public int TipoRespostaId { get; set; }
        public TipoResposta TipoResposta { get; set; }
        public IList<Resposta> Respostas { get; set; }
    }
}

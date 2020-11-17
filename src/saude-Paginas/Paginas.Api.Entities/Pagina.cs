using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paginas.Api.Entities
{
    public class Pagina : Base
    {
        public int CodigoExterno { get; set; }
        public byte[] Banner { get; set; }
        public string Apresentacao { get; set; }
        public string FabebookLink { get; set; }
        public string TwitterLink { get; set; }
        public string IntagramLink { get; set; }
    }
}

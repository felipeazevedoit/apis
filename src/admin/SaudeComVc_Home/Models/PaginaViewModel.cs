using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaudeComVoce.Models
{
    public class PaginaViewModel : BaseModel
    {
        public int CodigoExterno { get; set; }
        public byte[] Banner { get; set; }
        public string Apresentacao { get; set; }
        public string FabebookLink { get; set; }
        public string TwitterLink { get; set; }
        public string IntagramLink { get; set; }
    }
}
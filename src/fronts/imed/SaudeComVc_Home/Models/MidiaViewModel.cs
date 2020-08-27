using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SaudeComVoce.Models
{
    public class MidiaViewModel : BaseModel
    {
        public byte[] Arquivo { get; set; }
        public string CaminhoFisico { get; set; }
        public string CaminhoVirtual { get; set; }
        public int TipoId { get; set; }
        public int CategoriaId { get; set; }
        public string Extensao { get; set; }
        public int CodigoExterno { get; set; }
        public string ArquivoB64 { get; set; }
    }
}

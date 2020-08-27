using SaudeComVoce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaudeComVc_Home.Models
{
    public class NotificacaoViewModel : BaseModel
    {
        public int CodigoExterno { get; set; }
        public int NotificacaoStatusId { get; set; }
        public string Link { get; set; }
    }
}
using SaudeComVoce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaudeComVc_Home.Models
{
    public class MensagemViewModel : BaseModel
    {
        public int RemetenteId { get; set; }
        public int DestinatarioId { get; set; }
        public bool GerarNotificacao { get; set; }
        public NotificacaoViewModel Notificacao { get; set; }
        public string LinkNotificacao { get; set; }
    }
}
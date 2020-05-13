using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mensagens.Entities
{
    public class Mensagem : Base
    {
        public int RemetenteId { get; set; }
        public int DestinatarioId { get; set; }

        [NotMapped]
        public bool GerarNotificacao { get; set; }

        [NotMapped]
        public Notificacao Notificacao { get; set; }

        [NotMapped]
        public string LinkNotificacao { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mensagens.Entities
{
    public class Notificacao : Base
    {
        public int CodigoExterno { get; set; }
        public NotificacaoStatus NotificacaoStatusId { get; set; }
        public string Link { get; set; }

        public Notificacao()
        { }

        public Notificacao(string nome, string descricao, int usuarioCriacao, 
            int usuarioEdicao, int idCliente, int codigoExterno, NotificacaoStatus notificacaoStatusId, string link) 
            : base(nome, descricao, usuarioCriacao, usuarioEdicao, idCliente)
        {
            this.CodigoExterno = codigoExterno;
            this.NotificacaoStatusId = notificacaoStatusId;
            this.Link = link;
        }
    }

    public enum NotificacaoStatus
    {
        Pendente = 1,
        Visualizado = 2
    }
}

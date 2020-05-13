using Mensagens.Entities;
using Mensagens.Infrastructure.Exceptions;
using SaudeComVoce.Helpers;
using System;
using System.Threading.Tasks;

namespace Mensagens.Services
{
    public class NotificaoesService
    {
        public async Task<Notificacao> GerarNotificacaoAsync(Notificacao notificacao)
        {
            try
            {
                var helper = new ServiceHelper();
                var result = await helper.PostAsync<Notificacao>("http://notificacoes.talanservices.com.br/", "api/Notificacoes", notificacao);

                return result;
            }
            catch(Exception e)
            {
                throw new MensagensException("Não foi possível gerar a notificação para a mensagem informada.", e);
            }
        }
    }
}

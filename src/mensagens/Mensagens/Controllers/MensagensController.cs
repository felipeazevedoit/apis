using Mensagens.Domains;
using Mensagens.Entities;
using Mensagens.Infrastructure;
using Mensagens.Infrastructure.Exceptions;
using Mensagens.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Mensagens.Controllers
{
    public class MensagensController : ApiController
    {
        private readonly MensagensDomain _domain;
        private readonly NotificaoesService _nService;

        public MensagensController()
        {
            _domain = new MensagensDomain(new MensagensRepository());
            _nService = new NotificaoesService();
        }

        [HttpPost]
        [Route("api/Mensagens")]
        public async Task<HttpResponseMessage> SaveAsync([FromBody]Mensagem mensagem)
        {
            try
            {
                var result = _domain.Save(mensagem);

                if(result != null && result.ID > 0 && result.GerarNotificacao)
                {
                    try
                    {
                        var notificacao = await _nService.GerarNotificacaoAsync(new Notificacao(mensagem.Nome, "Você recebeu uma nova mensagem.",
                            mensagem.UsuarioCriacao, mensagem.UsuarioEdicao, mensagem.IdCliente, mensagem.DestinatarioId, NotificacaoStatus.Pendente, mensagem.LinkNotificacao));
                        mensagem.Notificacao = notificacao;
                    }
                    catch(Exception e)
                    {
                        //Não interfere na mensagem...
                    }
                }

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch(MensagensException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
            catch(Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpGet]
        [Route("api/Mensagens/{idCliente:int}/{mesangemId:int}")]
        public HttpResponseMessage GetById(int idCliente, int mesangemId)
        {
            try
            {
                var mensagem = _domain.GetById(mesangemId, idCliente);

                return Request.CreateResponse(HttpStatusCode.OK, mensagem);
            }
            catch (MensagensException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpGet]
        [Route("api/Mensagens/GetByRemetente/{idCliente:int}/{remetenteId:int}")]
        public HttpResponseMessage GetByIdRemetente(int idCliente, int remetenteId)
        {
            try
            {
                var mensagens = _domain.GetByIdRemetente(remetenteId, idCliente);

                return Request.CreateResponse(HttpStatusCode.OK, mensagens);
            }
            catch (MensagensException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpGet]
        [Route("api/Mensagens/GetByDestinatario/{idCliente:int}/{destinatarioId:int}")]
        public HttpResponseMessage GetByIdDestinatario(int idCliente, int destinatarioId)
        {
            try
            {
                var mensagens = _domain.GetByIdDestinatario(destinatarioId, idCliente);

                return Request.CreateResponse(HttpStatusCode.OK, mensagens);
            }
            catch (MensagensException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpGet]
        [Route("api/Mensagens/GetByEntidade/{idCliente:int}/{destinatarioId:int}/{remetenteId:int}")]
        public HttpResponseMessage GetByEntidadeAsync(int idCliente, int destinatarioId, int remetenteId)
        {
            try
            {
                var mensagens = _domain.GetByIdEntidade(destinatarioId, remetenteId, idCliente);

                return Request.CreateResponse(HttpStatusCode.OK, mensagens);
            }
            catch (MensagensException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}

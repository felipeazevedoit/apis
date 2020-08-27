using Formulario.Dinamico.Domains;
using Formulario.Dinamico.Entities;
using Formulario.Dinamico.Infrastructure;
using Formulario.Dinamico.Infrastructure.Exceptions;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Formulario.Dinamico.Controllers
{
    public class PerguntasController : ApiController
    {
        private readonly PerguntasDomain _domain;
        private readonly CriptografiaDomain _cDomain;

        public PerguntasController()
        {
            _domain = new PerguntasDomain(new PerguntasRepository());
            _cDomain = new CriptografiaDomain(new CriptografiasRepository());
        }

        [HttpGet]
        [Route("api/Perguntas/{idCliente:int}/{idExterno:int}")]
        public async Task<HttpResponseMessage> Get(int idCliente, int idExterno)
        {
            try
            {
                var perguntas = await _domain.GetAllAsync(idCliente, idExterno);               

                foreach (var pergunta in perguntas)
                {
                    if (pergunta.Respostas != null && pergunta.Respostas.Count() > 0)
                    {
                        var criptografia = _cDomain.GetByIdExterno(idExterno, idCliente);

                        if (criptografia == null || criptografia.ID == 0)
                        {
                            throw new CriptografiaException("Não foi possível descriptografar as respostas.");
                        }

                        foreach (var item in pergunta.Respostas)
                        {
                            var resposta = criptografia.Decrypt(item.Descricao);
                            var resposta2 = criptografia.Decrypt(item.Nome);

                            item.Descricao = resposta;
                            item.Nome = resposta2;
                        }
                    }
                }

                //return Request.CreateResponse(HttpStatusCode.OK, perguntas.FirstOrDefault().Respostas);
                return Request.CreateResponse(HttpStatusCode.OK, perguntas);
            }
            catch(PerguntaException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
            catch(Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Não foi possível completar a operação.");
            }
        }

        [HttpPost]
        [Route("api/Perguntas")]
        public HttpResponseMessage Save([FromBody]Pergunta pergunta)
        {
            try
            {
                var perg = _domain.Save(pergunta);

                return Request.CreateResponse(HttpStatusCode.OK, perg);
            }
            catch (PerguntaException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Não foi possível completar a operação.");
            }
        }

        [HttpGet]
        [Route("api/Perguntas/GetById/{idCliente:int}/{perguntaId:int}/{idExterno:int}")]
        public HttpResponseMessage GetById(int idCliente, int perguntaId, int idExterno)
        {
            try
            {
                var result = _domain.GetById(perguntaId, idCliente);

                if (result.Respostas != null && result.Respostas.Count() > 0)
                {
                    var criptografia = _cDomain.GetByIdExterno(idExterno, idCliente);

                    if (criptografia == null || criptografia.ID == 0)
                    {
                        throw new CriptografiaException("Não foi possível descriptografar as respostas.");
                    }

                    foreach (var item in result.Respostas)
                    {
                        var resposta = criptografia.Decrypt(item.Descricao);
                        var resposta2 = criptografia.Decrypt(item.Nome);

                        item.Descricao = resposta;
                        item.Nome = resposta2;
                    }
                }

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (PerguntaException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Não foi possível completar a operação.");
            }
        }
    }
}

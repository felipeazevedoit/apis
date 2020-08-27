using Formulario.Dinamico.Domains;
using Formulario.Dinamico.Entities;
using Formulario.Dinamico.Infrastructure;
using Formulario.Dinamico.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Formulario.Dinamico.Controllers
{
    public class RespostasController : ApiController
    {
        private readonly RespostasDomain _domain;
        private readonly CriptografiaDomain _cDomain;

        public RespostasController()
        {
            _domain = new RespostasDomain(new RespostasRepository());
            _cDomain = new CriptografiaDomain(new CriptografiasRepository());
        }

        [HttpPost]
        public HttpResponseMessage Save([FromBody]Resposta resposta)
        {
            try
            {
                var criptografia = _cDomain.GetByIdExterno(resposta.CodigoExterno, resposta.IdCliente);

                if(criptografia == null || criptografia.ID == 0)
                {
                    var key = RemoveSpecialCharacters($"{ resposta.EntidadeNome }_{ Guid.NewGuid().ToString() }").Substring(0, 16);
                    var nome = RemoveSpecialCharacters($"{ resposta.EntidadeNome }_{ resposta.CodigoExterno }_{ resposta.IdCliente }_{ Guid.NewGuid() }").Substring(0, 32);

                    criptografia = new Criptografia()
                    {
                        ChaveExterna = Encoding.UTF8.GetBytes(key),
                        Nome = Convert.ToBase64String(Encoding.UTF8.GetBytes(nome)),
                        Ativo = true,
                        CodigoExterno = resposta.CodigoExterno,
                        DataCriacao = DateTime.Now,
                        DateAlteracao = DateTime.Now,
                        IdCliente = resposta.IdCliente,
                        Status = 1,
                        UsuarioCriacao = resposta.UsuarioCriacao,
                        UsuarioEdicao = resposta.UsuarioEdicao,
                    };

                    criptografia = _cDomain.Save(criptografia);
                }

                var respostaCriptografada = criptografia.Encrypt(resposta.Descricao);
                //var respostaCriptografada2 = criptografia.Encrypt(resposta.Nome);

                if (!string.IsNullOrEmpty(respostaCriptografada))
                {
                    resposta.Descricao = respostaCriptografada;
                    var result = _domain.Save(resposta);

                    result.Descricao = criptografia.Decrypt(result.Descricao);

                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable,
                    new CriptografiaException("Não foi possível criptogrfar a resposta informada."));
            }
            catch(ApplicationException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, e);
            }
            catch (CriptografiaException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, e);
            }
            catch (RespostaException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Não foi possível completar a operação.");
            }
        }

        [HttpGet]
        [Route("api/Respostas/{idCliente:int}/{idExterno:int}/{perguntaId:int}")]
        public HttpResponseMessage GetByPergunta(int idCliente, int idExterno, int perguntaId)
        {
            try
            {
                var respostas = _domain.GetByPerguntaId(perguntaId, idCliente);

                if(respostas != null && respostas.Count() > 0)
                {
                    var criptografia = _cDomain.GetByIdExterno(idExterno, idCliente);

                    if(criptografia == null || criptografia.ID == 0)
                    {
                        throw new CriptografiaException("Não foi possível descriptografar as respostas.");
                    }

                    foreach (var item in respostas)
                    {
                        var resposta = criptografia.Decrypt(item.Descricao);
                        var resposta2 = criptografia.Decrypt(item.Nome);

                        item.Descricao = resposta;
                        item.Nome = resposta2;
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, respostas);
                }

                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, new CriptografiaException("Nenhuma resposta encontrada."));
            }
            catch (CriptografiaException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, e);
            }
            catch (RespostaException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Não foi possível completar a operação.");
            }
        }

        [HttpGet]
        [Route("api/Respostas/GetByIdExterno{idCliente:int}/{idExterno:int}")]
        public HttpResponseMessage GetByIdExterno(int idCliente, int idExterno)
        {
            try
            {
                var respostas = _domain.GetByIdExterno(idExterno, idCliente);

                if (respostas != null && respostas.Count() > 0)
                {
                    var criptografia = _cDomain.GetByIdExterno(idExterno, idCliente);

                    if (criptografia == null || criptografia.ID == 0)
                    {
                        throw new CriptografiaException("Não foi possível descriptografar as respostas.");
                    }

                    foreach (var item in respostas)
                    {
                        var resposta = criptografia.Decrypt(item.Descricao);
                        var resposta2 = criptografia.Decrypt(item.Nome);

                        item.Descricao = resposta;
                        item.Nome = resposta2;
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, respostas);
                }

                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, new CriptografiaException("Nenhuma resposta encontrada."));
            }
            catch (CriptografiaException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, e);
            }
            catch (RespostaException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Não foi possível completar a operação.");
            }
        }

        [HttpPost]
        [Route("api/Respostas/{idCliente:int}/{idExterno:int}")]
        public HttpResponseMessage GetByPerguntas(int idCliente, int idExterno, IEnumerable<int> perguntasIds)
        {
            try
            {
                var respostas = _domain.GetByPerguntasIds(perguntasIds, idCliente);

                if (respostas != null && respostas.Count() > 0)
                {
                    var criptografia = _cDomain.GetByIdExterno(idExterno, idCliente);

                    if (criptografia == null || criptografia.ID == 0)
                    {
                        throw new CriptografiaException("Não foi possível descriptografar as respostas.");
                    }

                    foreach (var item in respostas)
                    {
                        var resposta = criptografia.Decrypt(item.Descricao);
                        var resposta2 = criptografia.Decrypt(item.Nome);

                        item.Descricao = resposta;
                        item.Nome = resposta2;
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, respostas);
                }

                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, new CriptografiaException("Nenhuma resposta encontrada."));
            }
            catch (CriptografiaException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, e);
            }
            catch (RespostaException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Não foi possível completar a operação.");
            }
        }

        private string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }
}

using System;
using Microsoft.AspNetCore.Mvc;
using WebPixPrincipalRepository.Entity;
using Microsoft.AspNetCore.Cors;
using WebPixPrincipalBLL;
using WebPixPrincipalAPI.Helper;
using System.Threading.Tasks;

namespace WebPixPrincipalAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [EnableCors("AllowAll")]
    public class EmailController : Controller
    {
        [ActionName("EnviaSimplesEmail")]
        [HttpPost("{token}")]
        public async Task<Object> EnviaSimplesEmail([FromBody]Object envio, string token)
        {
            if (await Seguranca.validaTokenAsync(token))
            {
                dynamic obj = envio;

                Email email = new Email();

                email.Conteudo = obj.Conteudo;
                email.Titulo = obj.Titulo;
                string remetente = obj.remetente;
                string destinatario = obj.destinatario;
                int idCliente = obj.idCliente;

                EmailBO emailBO = new EmailBO();

                if (await emailBO.EnviaSimplesEmailAsync(email, remetente, destinatario, idCliente))
                {
                    Object Retorno = new object();
                    Retorno = new { retorno = "Email enviado com sucesso" };

                    return Retorno;
                }
                else
                {
                    Object Retorno = new object();
                    Retorno = new { retorno = "Houve uma falha ao enviar email" };

                    return Retorno;
                }
            }
            else
            {
                Object Retorno = new object();
                Retorno = new { retorno = "Você nao tem acesso esse plugin" };

                return Retorno;
            }


        }

        [ActionName("GetSimples")]
        [HttpGet]
        public string GetSimples()
        {
            return "teste";
        }
        
    }
}
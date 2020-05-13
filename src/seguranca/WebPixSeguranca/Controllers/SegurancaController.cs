using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebPixSeguranca.Model;
using SegurancaBO;
using WebPixSeguranca.Helper.Auxiliares;
using Microsoft.AspNetCore.Cors;
using System.Net;

namespace WebPixSeguranca.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors("AllowAll")]
    public class SegurancaController : Controller
    {
        // POST: api/Seguranca
        [HttpPost("{aux}/{acao}/{idcliente}/{idusuario}")]
        public async Task<JsonResult> Post(string aux, string acao, int idcliente, int idusuario, [FromBody]Object conteudo)
         {
            var ip = await GetIpAsync();

            var MotorAux = await Auxiliares.GetInfoMotorAux(aux, idcliente);
            AcaoViewModel acoesUsuario = MotorAux.Acoes.Where(x => x.Nome == acao).FirstOrDefault();

            if (await Auxiliares.VerificaUsuarioPermissaoAsync(acoesUsuario, idusuario, idcliente))
            {

                var token = TokenBO.GerateTokenValido(acoesUsuario.Caminho,idusuario,idcliente, ip);
                LogBo.Send(acao, acoesUsuario.Caminho, idusuario, idcliente, token.ID, ip);

                Object retorno = await Auxiliares.GetRetornoAuxAsync(MotorAux, acoesUsuario, token, conteudo,idcliente);



                return Json(retorno);
            }
            else
            {
                LogBo.Send(acao, acao, idusuario, idcliente,0, ip);

                return Json(new { error = "Houve um erro ou sem permissao" });

            }
        }


       

        // GET: api/Seguranca
        [HttpGet("{aux}/{acao}/{idcliente}/{idusuario}")]
        public async Task<JsonResult> Get(string aux, string acao, int idcliente, int idusuario)
        {
            var ip = await GetIpAsync();

            var MotorAux = await Auxiliares.GetInfoMotorAux(aux, idcliente);
            AcaoViewModel acoesUsuario = MotorAux.Acoes.Where(x => x.Nome == acao).FirstOrDefault();
            
            if (await Auxiliares.VerificaUsuarioPermissaoAsync(acoesUsuario, idusuario, idcliente))
            {
                var token = TokenBO.GerateTokenValido(acoesUsuario.Caminho, idusuario, idcliente, ip);
                LogBo.Send(acao, acoesUsuario.Caminho, idusuario, idcliente, token.ID, ip);

                Object retorno = await Auxiliares.GetRetornoAuxAsync(MotorAux, acoesUsuario, token, null, idcliente);

                return Json(retorno);
            }
            else
            {
                LogBo.Send(acao, acao, idusuario, idcliente, 0, ip);
                return Json(new { error = "Houve um erro ou sem permissao" });

            }
        }


        private async Task<string> GetIpAsync()
        {
            IPHostEntry heserver = await Dns.GetHostEntryAsync(Dns.GetHostName());//(Dns.GetHostName());
            //return heserver.AddressList[2].ToString();
            return string.Empty;
        }

    }
}

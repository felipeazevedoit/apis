using Microsoft.AspNetCore.Mvc;
using Entity;
using SegurancaBO;

namespace WebPixSeguranca.Controllers
{
    
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class TokenController : Controller
    {
        // GET: api/Token
        [HttpPost]
        public string GerarToken([FromBody]object objToken)
        {
            dynamic obj = objToken;
            var ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            var validade = TokenBO.GerateTokenValido(obj.UrlCliente, obj.idUsuario, obj.idCliente, ip);
            return validade;
        }

        [HttpGet("{guid}")]
        public bool ValidaToken(string guid,int acao, int aux)
        {
            try
            {
                var retorno  = TokenBO.ValidaToken(guid, acao, aux);

                if (retorno != null && retorno.ID != 0)
                {
                    LogBo.Send("ValidarToken", "tokem valido", retorno.idUsuario, retorno.idCliente, retorno.ID, "");

                    return true;
                }
                else
                {
                    LogBo.Send("ValidarToken", "token invalido", 0, 0, 0, "");

                    return false;
                }

            }
            catch (System.Exception ex)
            {
                LogBo.Send("ValidarToken ERROR", ex.Message, 0, 0, 0, "");
                throw;
            }
        }
    }
}

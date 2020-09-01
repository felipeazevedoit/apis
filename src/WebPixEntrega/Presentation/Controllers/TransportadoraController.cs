using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using wpEntity;
using DomainBusiness;
using System.Collections.Generic;

namespace WebPixTransportadora.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class TransportadoraController : Controller
    {
        [HttpPost("{token}")]
        [ActionName("SaveTransportadora")]
        public async Task<JsonResult> SaveTransportadora([FromBody]Transportadora Transportadora, string token)
        {
            if (await TransportadoraBO.SaveAsync(Transportadora, token))
                return Json("Configuracao salva com sucesso");
            else
                return Json("Encontramos algum problema ao salvar a Configuracao. Entre em contato com o suporte");
        }

        [ActionName("GetAllTransportadora")]
        [HttpGet("{idCliente}/{token}")]
        public async Task<IEnumerable<Transportadora>> GetAllTransportadora(int idCliente, string token)
        {
            return await TransportadoraBO.GetAllAsync(idCliente, token);
        }

        [ActionName("DeletarTransportadora")]
        [HttpPost("{token}")]
        public async Task<JsonResult> DeletarConfiguracao([FromBody]object Transportadora, string token)
        {

            if (await TransportadoraBO.RemoveAsync(Transportadora, token))
                return Json("Transportadora removido com sucesso");
            else
                return Json("Encontramos algum problema ao salvar a Configuracao. Entre em contato com o suporte");
        }
    }
}
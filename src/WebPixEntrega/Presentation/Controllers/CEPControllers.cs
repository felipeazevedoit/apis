using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using wpEntity;
using DomainBusiness;
using System.Collections.Generic;

namespace WebPixCEP.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class CEPController : Controller
    {
        [HttpPost("{token}")]
        [ActionName("SaveCEP")]
        public async Task<JsonResult> SaveCEP([FromBody]CEP CEP, string token)
        {
            if (await CEPBO.SaveAsync(CEP, token))
                return Json("Configuracao salva com sucesso");
            else
                return Json("Encontramos algum problema ao salvar a Configuracao. Entre em contato com o suporte");
        }

        [ActionName("GetAllCEP")]
        [HttpGet("{idCliente}/{token}")]
        public async Task<IEnumerable<CEP>> GetAllCEP(int idCliente, string token)
        {
            return await CEPBO.GetAllAsync(idCliente, token);
        }

        [ActionName("DeletarCEP")]
        [HttpPost("{token}")]
        public async Task<JsonResult> DeletarConfiguracao([FromBody]object CEP, string token)
        {

            if (await CEPBO.RemoveAsync(CEP, token))
                return Json("CEP removido com sucesso");
            else
                return Json("Encontramos algum problema ao salvar a Configuracao. Entre em contato com o suporte");
        }
    }
}
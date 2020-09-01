using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using wpEntity;
using DomainBusiness;
using System.Collections.Generic;

namespace WebPixPropiedade.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class PropiedadeController : Controller
    {
        [HttpPost("{token}")]
        [ActionName("SavePropiedade")]
        public async Task<JsonResult> SavePropiedade([FromBody]Propiedade Propiedade, string token)
        {
            if (await PropiedadeBO.SaveAsync(Propiedade, token))
                return Json("Configuracao salva com sucesso");
            else
                return Json("Encontramos algum problema ao salvar a Configuracao. Entre em contato com o suporte");
        }

        [ActionName("GetAllPropiedade")]
        [HttpGet("{idCliente}/{token}")]
        public async Task<IEnumerable<Propiedade>> GetAllPropiedade(int idCliente, string token)
        {
            return await PropiedadeBO.GetAllAsync(idCliente, token);
        }

        [ActionName("DeletarPropiedade")]
        [HttpPost("{token}")]
        public async Task<JsonResult> DeletarConfiguracao([FromBody]object Propiedade, string token)
        {

            if (await PropiedadeBO.RemoveAsync(Propiedade, token))
                return Json("Propiedade removido com sucesso");
            else
                return Json("Encontramos algum problema ao salvar a Configuracao. Entre em contato com o suporte");
        }
    }
}
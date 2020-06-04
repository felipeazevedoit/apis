using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Entity;
using DomainBusiness;
using System.Collections.Generic;

namespace WebPixPropiedades.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class PropiedadesController : Controller
    {
        [HttpPost("{token}")]
        [ActionName("SavePropiedades")]
        public async Task<JsonResult> SavePropiedades([FromBody]Propiedades Propiedades, string token)
        {
            if (await PropiedadesBO.SaveAsync(Propiedades, token))
                return Json("Configuracao salva com sucesso");
            else
                return Json("Encontramos algum problema ao salvar a Configuracao. Entre em contato com o suporte");
        }

        [ActionName("GetAllPropiedades")]
        [HttpGet("{idCliente}/{token}")]
        public async Task<IEnumerable<Propiedades>> GetAllPropiedades(int idCliente, string token)
        {
            return await PropiedadesBO.GetAllAsync(idCliente, token);
        }

        [ActionName("DeletarPropiedades")]
        [HttpPost("{token}")]
        public async Task<JsonResult> DeletarConfiguracao([FromBody]object Propiedades, string token)
        {

            if (await PropiedadesBO.RemoveAsync(Propiedades, token))
                return Json("Propiedades removido com sucesso");
            else
                return Json("Encontramos algum problema ao salvar a Configuracao. Entre em contato com o suporte");
        }
    }
}
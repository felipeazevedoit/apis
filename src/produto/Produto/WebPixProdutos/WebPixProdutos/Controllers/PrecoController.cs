using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Entity;
using DomainBusiness;
using System.Collections.Generic;

namespace WebPixPreco.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class PrecoController : Controller
    {
        [HttpPost("{token}")]
        [ActionName("SavePreco")]
        public async Task<JsonResult> SavePreco([FromBody]Preco Preco, string token)
        {
            if (await PrecoBO.SaveAsync(Preco, token))
                return Json("Configuracao salva com sucesso");
            else
                return Json("Encontramos algum problema ao salvar a Configuracao. Entre em contato com o suporte");
        }

        [ActionName("GetAllPreco")]
        [HttpGet("{idCliente}/{token}")]
        public async Task<IEnumerable<Preco>> GetAllPreco(int idCliente, string token)
        {
            return await PrecoBO.GetAllAsync(idCliente, token);
        }

        [ActionName("DeletarPreco")]
        [HttpPost("{token}")]
        public async Task<JsonResult> DeletarConfiguracao([FromBody]object Preco, string token)
        {

            if (await PrecoBO.RemoveAsync(Preco, token))
                return Json("Preco removido com sucesso");
            else
                return Json("Encontramos algum problema ao salvar a Configuracao. Entre em contato com o suporte");
        }
    }
}
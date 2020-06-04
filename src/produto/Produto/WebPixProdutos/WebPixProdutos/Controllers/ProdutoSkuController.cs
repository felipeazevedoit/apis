using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Entity;
using DomainBusiness;
using System.Collections.Generic;

namespace WebPixProdutoSku.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class ProdutoSkuController : Controller
    {
        [HttpPost("{token}")]
        [ActionName("SaveProdutoSku")]
        public async Task<JsonResult> SaveProdutoSku([FromBody]ProdutoSku ProdutoSku, string token)
        {
            if (await ProdutoSkuBO.SaveAsync(ProdutoSku, token))
                return Json("Configuracao salva com sucesso");
            else
                return Json("Encontramos algum problema ao salvar a Configuracao. Entre em contato com o suporte");
        }

        [ActionName("GetAllProdutoSku")]
        [HttpGet("{idCliente}/{token}")]
        public async Task<IEnumerable<ProdutoSku>> GetAllProdutoSku(int idCliente, string token)
        {
            return await ProdutoSkuBO.GetAllAsync(idCliente, token);
        }

        [ActionName("DeletarProdutoSku")]
        [HttpPost("{token}")]
        public async Task<JsonResult> DeletarConfiguracao([FromBody]object ProdutoSku, string token)
        {

            if (await ProdutoSkuBO.RemoveAsync(ProdutoSku, token))
                return Json("ProdutoSku removido com sucesso");
            else
                return Json("Encontramos algum problema ao salvar a Configuracao. Entre em contato com o suporte");
        }
    }
}
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Entity;
using DomainBusiness;
using System.Collections.Generic;

namespace WebPixProdutos.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class ProdutoController : Controller
    {
        [HttpPost("{token}")]
        [ActionName("SaveProduto")]
        public async Task<JsonResult> SaveProduto([FromBody]Produto produto, string token)
        {

            if (await ProdutoBO.SaveAsync(produto, token))
                return Json("Configuracao salva com sucesso");
            else
                return Json("Encontramos algum problema ao salvar a Configuracao. Entre em contato com o suporte");
        }

        [ActionName("GetAllProduto")]
        [HttpGet("{idCliente}/{token}")]
        public async Task<IEnumerable<Produto>> GetAllProduto(string idCliente, string token)
        {

            return await ProdutoBO.GetAllAsync(int.Parse(idCliente), token);
        }

        [ActionName("DeletarProduto")]
        [HttpPost("{token}")]
        public async Task<JsonResult> DeletarProduto([FromBody]object produto, string token)
        {

            if (await ProdutoBO.RemoveAsync(produto, token))
                return Json("Produto removido com sucesso");
            else
                return Json("Encontramos algum problema ao salvar a Configuracao. Entre em contato com o suporte");
        }
    }
}
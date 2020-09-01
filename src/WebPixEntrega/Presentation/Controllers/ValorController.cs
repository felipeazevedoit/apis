using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using wpEntity;
using DomainBusiness;
using System.Collections.Generic;

namespace WebPixValor.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class ValorController : Controller
    {
        [HttpPost("{token}")]
        [ActionName("SaveValor")]
        public async Task<JsonResult> SaveValor([FromBody]Valor Valor, string token)
        {
            if (await ValorBO.SaveAsync(Valor, token))
                return Json("Configuracao salva com sucesso");
            else
                return Json("Encontramos algum problema ao salvar a Configuracao. Entre em contato com o suporte");
        }

        [ActionName("GetAllValor")]
        [HttpGet("{idCliente}/{token}")]
        public async Task<IEnumerable<Valor>> GetAllValor(int idCliente, string token)
        {
            return await ValorBO.GetAllAsync(idCliente, token);
        }

        [ActionName("DeletarValor")]
        [HttpPost("{token}")]
        public async Task<JsonResult> DeletarValor([FromBody]object Valor, string token)
        {

            if (await ValorBO.RemoveAsync(Valor, token))
                return Json("Valor removido com sucesso");
            else
                return Json("Encontramos algum problema ao salvar a Configuracao. Entre em contato com o suporte");
        }

        [ActionName("BuscarValorEntrega")]
        [HttpPost("{token}")]
        public async Task<JsonResult> BuscarValorEntrega([FromBody]object Propiedades, string token)
        {
            dynamic proper = Propiedades;
            return await ValorBO.CalcutatePACFromServiceAsync(proper.IDProduto, proper.CEP, proper.idCliente, proper.idUsuario, token);
        }
    }
}
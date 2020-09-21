using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WpPagamento.Dominio;
using WpPagamentos.API.Helper;
using WpPagamentos.Entidade;

namespace WpPagamentos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagamentoController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> RealizarPagamentoAsync([FromBody]Loja loja, string token)
        {

            if (await Seguranca.validaTokenAsync(token))
            {
                if (loja.idCliente != 0)
                {
                    PagamentoBO pagamento = new PagamentoBO();
                    if (string.IsNullOrEmpty(await pagamento.GerarPagamentoSimplesErede(loja)))
                        return Ok("Pagamento realizado com sucesso");
                    else
                        return Ok("Encontramos algum problema ao salvar a Arquivo. Entre em contato com o suporte");

                }
                else
                    return Ok("Encontramos algum problema ao salvar a Arquivo. Entre em contato com o suporte");
            }
            else
                return Ok("Você nao tem acesso neste plugin");


        }
    }
}

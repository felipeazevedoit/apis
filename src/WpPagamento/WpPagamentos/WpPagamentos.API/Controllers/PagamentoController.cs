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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PagamentoController : ControllerBase
    {
        [HttpPost("{token}")]
        public async Task<string> RealizarPagamento([FromBody] Loja loja, string token)
        {
           
            if (await Seguranca.validaTokenAsync(token))
            {
                if (loja.idCliente != 0)
                {
                    PagamentoBO pagamento = new PagamentoBO();
                    if (await pagamento.GerarPagamentoSimplesErede(loja) == true)
                        return "Pagamento realizado com sucesso";
                    else
                        return "Encontramos algum problema realizar o pagamento. Entre em contato com o suporte";

                }
                else
                    return "Encontramos algum problema ao realizar o pagamento. Entre em contato com o suporte";
            }
            else
                return "Você nao tem acesso neste plugin";


        }
    }
}

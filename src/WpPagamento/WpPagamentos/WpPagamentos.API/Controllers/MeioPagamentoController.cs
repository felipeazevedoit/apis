using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WpPagamento.Dominio;
using WpPagamentos.Entidade;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WpPagamentos.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MeioPagamentoController : ControllerBase
    {
        // GET: api/<MeioPagamentoController>
        [HttpGet("{idCliente}/{token}")]
        public async Task<IEnumerable<MeioPagamento>> GetAllMeioPagamento(int idCliente, string token)
        {
            return await MeioPagamentoBO.GetAllAsync(idCliente, token);
        }

        // POST api/<MeioPagamentoController>
        [HttpPost("{token}")]
        [ActionName("SaveMeioPagamento")]
        public async Task<object> SavePagamentoAsync([FromBody]MeioPagamento meioPagamento, string token)
        {
            try
            {
                var ret = await MeioPagamentoBO.SaveAsync(meioPagamento, token);
                if (ret != null)
                    return ret;
                else
                    return null;
            }
            catch(Exception e)
            {
                return null;
            }
           
        }
    }
}

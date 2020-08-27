using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace WebPixSeguranca.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class PermissaoController : Controller
    {
        [HttpGet("{idCliente}")]
        public List<Permissao> GetAllPermissao(int idCliente)
        {
            return PermissaoDAO.GetAll().Where(x => x.idCliente == idCliente).ToList();
        }

        [HttpGet("{idUsuario}")]
        public async Task<Permissao> GetPermissoesPorUsuario(int idUsuario)
        {
           return await PermissaoDAO.CarregarPermissaoByUsuarioAsync(idUsuario);
        }

        [ActionName("BuscaPermissoesPorIds")]
        [HttpPost]
        public async Task<IEnumerable<Permissao>> GetPermissoesAsync([FromBody]IEnumerable<int> ids)
        {
            return await PermissaoDAO.GetByIdsAsync(ids);
        }
    }
}
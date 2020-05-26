using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebPixPrincipalRepository.Entity;
using WebPixPrincipalRepository;
using WebPixPrincipalAPI.Helper;
using System.Threading.Tasks;

namespace WebPixPrincipalAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class PerfilController : Controller
    {
        [ActionName("SavePerfil")]
        [HttpPost]
        public string SavePerfil([FromBody]Perfil perfil)
        {
            if (perfil.idCliente != 0)
            {
                if (PerfilDAO.Save(perfil))
                {
                    return "Perfil salva com sucesso";
                }
                else
                {
                    return "Encontramos algum problema ao salvar o Perfil. Entre em contato com o suporte";
                }
            }
            else
                return "Encontramos algum problema ao salvar o Perfil. Entre em contato com o suporte";
        }

        [ActionName("GetAllPerfil")]
        [HttpGet("{idCliente:int}/{token}")]
        public async Task<IEnumerable<Perfil>> GetAllPerfil([FromRoute]int idCliente, [FromRoute]string token)
        {
            if (await Seguranca.validaTokenAsync(token))
            {
                return PerfilDAO.GetAll().Where(x => x.idCliente == idCliente).ToList();
            }

            return new List<Perfil>();
        }

        [ActionName("GetPerfilByID")]
        [HttpGet("{id:int}/{token}")]
        public async Task<Perfil> GetPerfilByID([FromRoute]int id, [FromRoute]string token)
        {
            if (await Seguranca.validaTokenAsync(token))
            {
                return PerfilDAO.GetById(id);
            }
            else
            {
                return new Perfil();
            }
        }

        //[ActionName("DeletePerfil")]
        //[HttpDelete("{id}")]
        //public string Delete(int id)
        //{
        //    var Perfil = PerfilDAO.GetAll().Find(x => x.ID == id);
        //    if (PerfilDAO.Remove(Perfil))
        //    {
        //        return "Pagina Deletada com sucesso";
        //    }
        //    else
        //    {
        //        return "Encontramos algum problema ao salvar a pagina. Entre em contato com o suporte";
        //    }
        //}
    }
}
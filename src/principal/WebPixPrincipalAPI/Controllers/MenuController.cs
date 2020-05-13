using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebPixPrincipalRepository.Entity;
using WebPixPrincipalRepository;
using Microsoft.AspNetCore.Cors;
using WebPixPrincipalAPI.Helper;
using System.Threading.Tasks;
using System;

namespace WebPixPrincipalAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [EnableCors("AllowAll")]
    public class MenuController : Controller
    {
        [ActionName("SaveMenu")]
        [HttpPost("{token}")]
        public async Task<JsonResult> SaveMenu([FromBody]Menu Menu, string token)
        {
            if (await Seguranca.validaTokenAsync(token))
            {
                if (Menu.idCliente != 0)
                {
                    if (MenuDAO.Save(Menu))
                    {
                        return Json("Menu salva com sucesso");
                    }
                    else
                    {
                        return Json("Encontramos algum problema ao salvar o Menu. Entre em contato com o suporte");
                    }
                }
                else
                    return Json("Encontramos algum problema ao salvar o Menu. Entre em contato com o suporte");
            }
            else
                return Json("Você nao tem acesso a esse plugin");
        }

        [ActionName("GetAllMenu")]
        [HttpGet("{idCliente}/{token}")]
        public async Task<IEnumerable<Menu>> GetAllMenu(int idCliente, string token)
        {
            if (await Seguranca.validaTokenAsync(token))
                return MenuDAO.GetAll().Where(x => x.idCliente == idCliente).ToList();
            else
                return new List<Menu>();
        }

        [ActionName("DeletarMenu")]
        [HttpPost("{token}")]
        public async Task<JsonResult> DeletarMenu([FromBody]object Menu, string token)
        {
            dynamic objEn = Menu;
            string a = objEn.idMenu.ToString();
            if (await Seguranca.validaTokenAsync(token))
            {
                Menu obj = MenuDAO.GetAll().Where(x => x.ID == Convert.ToInt32(a)).FirstOrDefault();
                return Json(new { msg = MenuDAO.Remove(obj) });
                //return Json(new { msg = false });
            }
            else
                return Json(new { msg = false });
        }
    }
}
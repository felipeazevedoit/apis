using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebPixPrincipalAPI.Helper;
using WebPixPrincipalRepository;
using WebPixPrincipalRepository.Entity;

namespace WebPixPrincipalAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class TemaController : Controller
    {
        [HttpPost("{token}")]
        [ActionName("SaveTema")]
        public async Task<JsonResult> SaveTema([FromBody]Tema Tema, string token)
        {
            if (await Seguranca.validaTokenAsync(token))
            {
                if (Tema.idCliente != 0)
                {
                    if (TemaDAO.Save(Tema))
                    {
                        return Json("Tema salva com sucesso");
                    }
                    else
                    {
                        return Json("Encontramos algum problema ao salvar a Tema. Entre em contato com o suporte");
                    }
                }
                else
                    return Json("Encontramos algum problema ao salvar a Tema. Entre em contato com o suporte");
            }
            else
            {
                return Json("Você nao tem acesso neste plugin");
            }
        }

        [ActionName("GetAllTema")]
        [HttpGet("{idCliente}/{token}")]
        public async Task<IEnumerable<Tema>> GetAllTema(int idcliente, string token)
        {
            if (await Seguranca.validaTokenAsync(token))
            {
                var aa = TemaDAO.GetAll();
                return aa;
            }
            else
                return new List<Tema>();
        }

        [ActionName("GetTema")]
        [HttpGet("{idTema}/{idCliente}/{token}")]
        public async Task<IEnumerable<Tema>> GetTema(int idcliente, int idTema, string token)
        {
            if (await Seguranca.validaTokenAsync(token))
            {
                return TemaDAO.GetAll().Where(x => x.idCliente == idcliente && x.ID == idTema).ToList();
            }
            else
                return new List<Tema>();
        }

        [ActionName("DeletarTema")]
        [HttpPost("{token}")]
        public async Task<JsonResult> DeletarTema([FromBody]object Tema, string token)
        {
            dynamic objEn = Tema;
            string a = objEn.idTema.ToString();
            if (await Seguranca.validaTokenAsync(token))
            {
                Tema obj = TemaDAO.GetAll().Where(x => x.ID == Convert.ToInt32(a)).FirstOrDefault();
                return Json(new { msg = TemaDAO.Remove(obj) });
                //return Json(new { msg = false });
            }
            else
                return Json(new { msg = false });
        }
    }
}
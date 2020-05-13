using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebPixPrincipalRepository.Entity;
using WebPixPrincipalRepository;
using System.Threading.Tasks;
using WebPixPrincipalAPI.Helper;

namespace WebPixPrincipalAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class EstiloController : Controller
    {
        [HttpPost("{token}")]
        [ActionName("SaveEstilo")]
        public async Task<JsonResult> SaveEstilo([FromBody]Estilo estilo, string token)
        {
            if (await Seguranca.validaTokenAsync(token))
            {
                if (estilo.idCliente != 0)
                {
                    if (EstiloDAO.Save(estilo))
                    {
                        return Json("Estilo salva com sucesso");
                    }
                    else
                    {
                        return Json("Encontramos algum problema ao salvar a Estilo. Entre em contato com o suporte");
                    }
                }
                else
                {
                    return Json("Conteudo de estilo esta incompleto");
                }
            }
            else
                return Json("Você nao tem acesso e esse plugin");
        }

        [ActionName("GetAllEstilo")]
        [HttpGet("{idCliente}/{token}")]
        public async Task<IEnumerable<Estilo>> GetAllEstilo(int idCliente, string token)
        {
            if (await Seguranca.validaTokenAsync(token))
            {
                return EstiloDAO.GetAll().Where(x => x.idCliente == idCliente).ToList();
            }
            else
                return new List<Estilo>();
        }

        [ActionName("DeletarEstilo")]
        [HttpPost("{token}")]
        public async Task<JsonResult> DeletarEstilo([FromBody]object Estilo, string token)
        {
            dynamic objEn = Estilo;
            string a = objEn.idEstilo.ToString();
            if (await Seguranca.validaTokenAsync(token))
            {
                Estilo obj = EstiloDAO.GetAll().Where(x => x.ID == Convert.ToInt32(a)).FirstOrDefault();
                return Json(new { msg = EstiloDAO.Remove(obj) });
                //return Json(new { msg = false });
            }
            else
                return Json(new { msg = false });
        }



        //[HttpPost]
        //[ActionName("GetEstiloByNome")]
        //public Estilo GetEstiloByNome([FromBody]object url)
        //{
        //    //Implantacao
        //    throw new NotImplementedException();

        //}

        //[ActionName("GetEstiloByID")]
        //[HttpGet("{id}")]
        //public Estilo GetEstiloByID(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //[ActionName("DeleteEstilo")]
        //[HttpDelete("{id}")]
        //public string Delete(int id)
        //{
        //    throw new NotImplementedException();
        //    //var Estilo = EstiloDAO.GetAll().Find(x => x.ID == id);
        //    //if (EstiloDAO.Remove(Estilo))
        //    //{
        //    //    return "Estilo Deletada com sucesso";
        //    //}
        //    //else
        //    //{
        //    //    return "Encontramos algum problema ao salvar a Estilo. Entre em contato com o suporte";
        //    //}
        //}
    }
}

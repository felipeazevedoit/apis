using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebPixPrincipalRepository.Entity;
using WebPixPrincipalAPI.Helper;
using WebPixPrincipalRepository;

namespace WebPixPrincipalAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class ArquivoController : Controller
    {

        [HttpPost("{token}")]
        [ActionName("SaveArquivo")]
        public async Task<JsonResult> SaveArquivo([FromBody]Arquivo Arquivo, string token)
        {
            if (await Seguranca.validaTokenAsync(token))
            {
                if (Arquivo.idCliente != 0)
                {
                    if (ArquivoDAO.Save(Arquivo))
                        return Json("Arquivo salva com sucesso");
                    else
                        return Json("Encontramos algum problema ao salvar a Arquivo. Entre em contato com o suporte");

                }
                else
                    return Json("Encontramos algum problema ao salvar a Arquivo. Entre em contato com o suporte");
            }
            else
                return Json("Você nao tem acesso neste plugin");
        }

        [ActionName("GetAllArquivo")]
        [HttpGet("{idCliente}/{token}")]
        public async Task<IEnumerable<Arquivo>> GetAllArquivo(int idcliente, string token)
        {
            if (await Seguranca.validaTokenAsync(token))
            {
                var aa = ArquivoDAO.GetAll().Where(x => x.idCliente == idcliente).ToList();
                return aa;
            }
            else
                return new List<Arquivo>();
        }

        [ActionName("GetArquivo")]
        [HttpGet("{idArquivo}/{idCliente}/{token}")]
        public async Task<IEnumerable<Arquivo>> GetArquivo(int idcliente, int idArquivo, string token)
        {
            if (await Seguranca.validaTokenAsync(token))
            {
                return ArquivoDAO.GetAll().Where(x => x.idCliente == idcliente && x.ID == idArquivo).ToList();
            }
            else
                return new List<Arquivo>();
        }

        [ActionName("DeletarArquivo")]
        [HttpPost("{token}")]
        public async Task<JsonResult> DeletarArquivo([FromBody]object Arquivo, string token)
        {
            dynamic objEn = Arquivo;
            string a = objEn.idArquivo.ToString();
            if (await Seguranca.validaTokenAsync(token))
            {
                Arquivo obj = ArquivoDAO.GetAll().Where(x => x.ID == Convert.ToInt32(a)).FirstOrDefault();
                return Json(new { msg = ArquivoDAO.Remove(obj) });
                //return Json(new { msg = false });
            }
            else
                return Json(new { msg = false });
        }
    }
}
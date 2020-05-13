using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebPixPrincipalAPI.Helper;
using WebPixPrincipalRepository.Entity;
using WebPixPrincipalRepository;
using System;

namespace WebPixPrincipalAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class EstruturaController : Controller
    {
        [HttpPost("{token}")]
        [ActionName("SaveEstrutura")]
        public async Task<JsonResult> SaveEstrutura([FromBody]Estrutura Estrutura, string token)
        {
            if (await Seguranca.validaTokenAsync(token))
            {
                if (Estrutura.idCliente != 0)
                {
                    if (EstruturaDAO.Save(Estrutura))
                    {
                        return Json("Estrutura salva com sucesso");
                    }
                    else
                    {
                        return Json("Encontramos algum problema ao salvar a Estrutura. Entre em contato com o suporte");
                    }
                }
                else
                {
                    return Json("Conteudo de Estrutura esta incompleto");
                }
            }
            else
                return Json("Você nao tem acesso e esse plugin");
        }

        [ActionName("GetAllEstrutura")]
        [HttpPost("{idCliente}/{token}")]
        public async Task<IEnumerable<Estrutura>> GetAllEstrutura([FromRoute]int idCliente,
            [FromBody]IEnumerable<int> idTipoAcoes, [FromRoute]string token)
        {
            if (await Seguranca.validaTokenAsync(token))
            {
                return EstruturaDAO.GetAll(idCliente, idTipoAcoes);
            }
            else
                return new List<Estrutura>();
        }

        [ActionName("DeletarEstrutura")]
        [HttpPost("{token}")]
        public async Task<JsonResult> DeletarEstrutura([FromBody]object Estrutura, string token)
        {
            dynamic objEn = Estrutura;
            string a = objEn.idEstrutura.ToString();
            if (await Seguranca.validaTokenAsync(token))
            {
                Estrutura obj = EstruturaDAO.GetAll().Where(x => x.ID == Convert.ToInt32(a)).FirstOrDefault();
                return Json(new { msg = EstruturaDAO.Remove(obj) });
                //return Json(new { msg = false });
            }
            else
                return Json(new { msg = false });
        }

        [ActionName("GetEstruturas")]
        [HttpPost("{idCliente:int}/{tipo:int}/{token}")]
        public async Task<IEnumerable<Estrutura>> GetEstruturasAsync([FromRoute]int idCliente, [FromRoute] int tipo,
            [FromRoute]string token, [FromBody]IDictionary<string, string> valuePairs)
        {
            if(await Seguranca.validaTokenAsync(token))
            {
                List<Estrutura> result = new List<Estrutura>();

                foreach (var item in valuePairs)
                {
                    var tipoAcoes = item.Value.Split(',').Select(t => Convert.ToInt32(t));
                    var estruturas = EstruturaDAO.GetByMotorAndTipoAcoes(Convert.ToInt32(item.Key), tipoAcoes, tipo, idCliente);

                    result.AddRange(estruturas);
                }

                return result;
            }
            else
            {
                return new List<Estrutura>();
            }
        }
    }
}
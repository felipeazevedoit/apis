using System;
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
    public class ConfiguracaoController : Controller
    {
        [HttpPost("{token}")]
        [ActionName("SaveConfiguracao")]
        public async Task<JsonResult> SaveConfiguracao([FromBody]Configuracao Configuracao,string token)
        {
            if (await Seguranca.validaTokenAsync(token))
            {
                if (Configuracao.idCliente != 0)
                {
                    if (ConfiguracaoDAO.Save(Configuracao))
                    {
                        return Json("Configuracao salva com sucesso");
                    }
                    else
                    {
                        return Json("Encontramos algum problema ao salvar a Configuracao. Entre em contato com o suporte");
                    }
                }
                else
                {
                    return Json("Conteudo de Configuracao esta incompleto");
                }
            }
            else
            {
                return Json("Você nao tem acesso a este plugin");
            }
        }

        [ActionName("GetAllConfiguracao")]
        [HttpGet("{idCliente}/{token}")]
        public async Task<IEnumerable<Configuracao>> GetAllConfiguracao(int idCliente,string token)
        {
            if (await Seguranca.validaTokenAsync(token))
            {
                var config = ConfiguracaoDAO.GetAll().Where(x => x.idCliente == idCliente).ToList();
                return config;
            }
            else
                return new List<Configuracao>();
        }

        [ActionName("DeletarConfiguracao")]
        [HttpPost("{token}")]
        public async Task<JsonResult> DeletarConfiguracao([FromBody]object Configuracao, string token)
        {
            dynamic objEn = Configuracao;
            string a = objEn.idConfiguracao.ToString();
            if (await Seguranca.validaTokenAsync(token))
            {
                Configuracao obj = ConfiguracaoDAO.GetAll().Where(x => x.ID == Convert.ToInt32(a)).FirstOrDefault();
                return Json(new { msg = ConfiguracaoDAO.Remove(obj) });
                //return Json(new { msg = false });
            }
            else
                return Json(new { msg = false });
        }

    }
}
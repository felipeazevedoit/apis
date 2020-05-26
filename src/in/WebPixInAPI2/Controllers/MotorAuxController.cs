using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Entity;
using WebPixInAPI.Model;
using Repository;
using System.Linq;

namespace WebPixInAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class MotorAuxController : Controller
    {
        [HttpGet("{aux}")]
        [ActionName("acessarmotor")]
        public MotorAuxViewModel acessarmotor(string aux)
        {
            MotorAux motor = MotorAuxDAO.GetAll().Where(x => x.Nome == aux).FirstOrDefault();
            MotorAuxViewModel retorno = new MotorAuxViewModel();
            List<AcaoViewModel> listAcao = new List<AcaoViewModel>();

            foreach (Acao acao1 in AcaoDAO.GetAll().Where(x => x.idMotorAux == motor.ID).ToList())
            {
                AcaoViewModel acaoFil = new AcaoViewModel();
                acaoFil.ID = acao1.ID;
                acaoFil.Nome = acao1.Nome;
                acaoFil.Descricao = acao1.Descricao;
                acaoFil.Ativo = acao1.Ativo;
                acaoFil.Status = acao1.Status;
                acaoFil.Caminho = acao1.Caminho;
                acaoFil.idMotorAux = acao1.idMotorAux;
                acaoFil.TipoAcao = acao1.idTipoAcao;

                List<ParametroViewModel> listParametro = new List<ParametroViewModel>();
                foreach (Parametro parametro in ParametroDAO.GetAll().Where(x => x.idAcao == acao1.ID).ToList())
                {
                    ParametroViewModel parametroFil = new ParametroViewModel();

                    parametroFil.idAcao = parametro.idAcao;
                    parametroFil.Ordem = parametro.Ordem;
                    parametroFil.Tipo = parametro.Tipo;
                    parametroFil.Ativo = parametro.Ativo;
                    parametroFil.Descricao = parametro.Descricao;
                    parametroFil.ID = parametro.ID;
                    parametroFil.Nome = parametro.Nome;
                    parametroFil.Status = parametro.Status;
                    parametroFil.Tipo = parametro.Tipo;

                    listParametro.Add(parametroFil);

                }

                acaoFil.Parametro = listParametro;

                listAcao.Add(acaoFil);
            }
            
            retorno.ID = motor.ID;
            retorno.Nome = motor.Nome;
            retorno.Descricao = motor.Descricao;
            retorno.Ativo = true;
            retorno.Status = 1;
            retorno.idCliente = motor.idCliente;
            retorno.Url = motor.Url;
            retorno.Acoes = listAcao;

            return retorno;
            
        }

        [ActionName("GetAll")]
        public List<MotorAux> GetAll()
        {
            return MotorAuxDAO.GetAll();
        }

        [HttpPost]
        [ActionName("Save")]
        public string Save([FromBody]MotorAux motorAux)
        {
            return MotorAuxDAO.Save(motorAux);
        }
    }
}
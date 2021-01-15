using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WpComunicacoes.Entidades;
using WpComunic.Dominio;
using WpComunic.Repositorio;
using Microsoft.AspNetCore.Cors;

namespace WpComunic.Apresentacao.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class AcessoController : ControllerBase
    {

        [HttpPost("{motorExterno}/{metodoId}")]
        [ActionName("RealizaComunicacao")]
        public async Task<string> RealizarComunicacao([FromBody]IEnumerable<Propriedades> propriedades, int motorExterno, int metodoId)
        {

            var Lista = propriedades.OrderBy(x => x.Ordem).ToList();

            MotorExternoRep rep = new MotorExternoRep();
            MotorExterno motor = rep.GetMotorExterno(motorExterno);

            MetodoRep metodoRep = new MetodoRep();
            Metodo metodo = motor.metodo.Where(x => x.ID == metodoId).FirstOrDefault();
            metodo.ClasseEntrada.propriedades = propriedades.ToList();

            motor.metodo = new List<Metodo>();
            motor.metodo.Add(metodo);

            ComunicacaoBO comunicacao = new ComunicacaoBO();
            var ret = comunicacao.RealizaComunicacao(motor);
            return ret;
        }
    }
}

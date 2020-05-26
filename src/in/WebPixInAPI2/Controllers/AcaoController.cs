using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entity;
using Repository;

namespace WebPixInAPI2.Controllers
{
    [Produces("application/json")]
    [Route("api/Acao")]
    public class AcaoController : Controller
    {
        // GET: api/Acao
        [HttpGet]
        public IEnumerable<Acao> Get()
        {
            return AcaoDAO.GetAll();
        }
        
        // POST: api/Acaos
        [HttpPost]
        public string Post([FromBody]Acao value)
        {
            return AcaoDAO.Save(value);
        }
    }
}

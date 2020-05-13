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
    [Route("api/Parametro")]
    public class ParametroController : Controller
    {
        // GET: api/Parametro
        [HttpGet]
        public IEnumerable<Parametro> Get()
        {
            return ParametroDAO.GetAll();
        }

        
        // POST: api/Parametro
        [HttpPost]
        public string Post([FromBody]Parametro value)
        {
            return ParametroDAO.Save(value);
        }
        
       
    }
}

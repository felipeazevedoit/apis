using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Entity;
using WebPixRepository;

namespace WebPixInAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Cliente")]
    public class ClienteController : Controller
    {
        // GET: api/Cliente
        [HttpGet]
        public IEnumerable<Cliente> Get()
        {
            return  ClienteDAO.GetAll();
        }
        
        // POST: api/Cliente
        [HttpPost]
        public string Post([FromBody]Cliente cliente)
        {
            return ClienteDAO.Save(cliente);
        }
    }
}

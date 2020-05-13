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

        // GET: api/Cliente/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Cliente
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Cliente/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

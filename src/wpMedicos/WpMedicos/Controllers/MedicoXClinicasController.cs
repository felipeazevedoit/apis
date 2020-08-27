using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WpMedicos.Domains;
using WpMedicos.Infrastructure.Exceptions;
using WpNoticias.Services;

namespace WpMedicos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoXClinicasController : ControllerBase
    {
        private readonly MedicoXClinicasDomain _domain;
        private readonly SegurancaService _service;
        public MedicoXClinicasController([FromServices]MedicoXClinicasDomain domain, [FromServices]SegurancaService service)
        {
            _domain = domain;
            _service = service;
        }
        [HttpPost("{idCliente:int}/{token}")]
        public async Task<IActionResult> BuscarPorIdsAsync([FromRoute]int idCliente, [FromRoute]string token, [FromBody]int id)
        {
            try
            {
                await _service.ValidateTokenAsync(token);
                var result = _domain.GetByMedicoId(id);
                return Ok(result);
            }
            catch (ServiceException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (InvalidTokenException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (MedicoXClinicasException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WpMedicos.Domains;
using WpMedicos.Infrastructure.Exceptions;
using WpNoticias.Services;

namespace WpMedicos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicosXEspecialidadesController : ControllerBase
    {
        private readonly MedicosXEspecialidadesDomain _domain;
        private readonly SegurancaService _service;

        public MedicosXEspecialidadesController([FromServices]MedicosXEspecialidadesDomain domain, [FromServices]SegurancaService service)
        {
            _domain = domain;
            _service = service;
        }

        [HttpPost("BuscarPorIdsAsync/{idCliente:int}/{token}")]
        public async Task<IActionResult> BuscarPorIdsAsync([FromRoute]int idCliente, [FromRoute]string token, [FromBody]IEnumerable<int> ids)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = _domain.GetByIds(ids, idCliente);

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
            catch (MedicosXEspecialidadesException e)
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
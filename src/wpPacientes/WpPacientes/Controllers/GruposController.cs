using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WpPacientes.Domains;
using WpPacientes.Infrastructure.Exceptions;
using WpPacientes.Services;

namespace WpPacientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GruposController : ControllerBase
    {
        private readonly SegurancaService _service;
        private readonly GrupoDomain _domain;

        public GruposController(SegurancaService service, GrupoDomain domain)
        {
            _service = service;
            _domain = domain;
        }

        [HttpGet("{idCliente:int}/{token}")]
        public async Task<IActionResult> GetAsync(int idCliente, string token)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = _domain.GetAll(idCliente);
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
            catch (GrupoException e)
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
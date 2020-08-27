using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WpNoticias.Domains;
using WpNoticias.Entities;
using WpNoticias.Infrastructure.Exceptions;
using WpNoticias.Services;

namespace WpNoticias.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoticiaXPacienteController : ControllerBase
    {
        private readonly SegurancaService _service;
        private readonly NoticiaXPacienteDomain _domain;

        public NoticiaXPacienteController([FromServices]SegurancaService service, [FromServices]NoticiaXPacienteDomain domain)
        {
            _service = service;
            _domain = domain;
        }

        [HttpPost("{token}")]
        public async Task<IActionResult> SaveAsync([FromRoute]string token, [FromBody]NoticiaXPaciente nXp)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = _domain.Save(nXp);
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
            catch (NoticiaXPacienteException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpPost("NoticiaId/{idCliente:int}/{token}")]
        public async Task<IActionResult> GetByNoticiaIdAsync([FromBody]int noticiaId, [FromRoute]int idCliente, [FromRoute]string token)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = _domain.GetByNoticiaId(noticiaId);
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
            catch (NoticiaXPacienteException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpPost("PacienteId/{idCliente:int}/{token}")]
        public async Task<IActionResult> GetByPacienteId([FromBody]int pacienteId, [FromRoute]int idCliente, [FromRoute]string token)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = _domain.GetByPacienteId(pacienteId);
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
            catch (NoticiaXPacienteException e)
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
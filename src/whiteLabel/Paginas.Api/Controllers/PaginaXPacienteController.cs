    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Paginas.Api.Domains;
using Paginas.Api.Entities;
using Paginas.Api.Infrastructure;
using Paginas.Api.Infrastructure.Exceptions;
using Paginas.Api.Services;

namespace Paginas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaginaXPacienteController : ControllerBase
    {
        private readonly SegurancaService _service;
        private readonly PaginaXPacienteDomain _domain;

        public PaginaXPacienteController()
        {
            _service = new SegurancaService();
            _domain = new PaginaXPacienteDomain(new PaginaXPacienteRepository());
        }

        [HttpPost]
        [Route("{token}")]
        public async Task<IActionResult> SaveAsync(string token, [FromBody]PaginaXPaciente pagina)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var pg = _domain.Save(pagina);

                return Ok(pg);
            }
            catch (InvalidTokenException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (ServiceException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (PaginaXPacienteException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet]
        [Route("BuscarPagina/{idPagina:int}/{token}")]
        public async Task<IActionResult> GetPaginaIdAsync(string token, int idPagina)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = _domain.GetByPaginaId(idPagina);

                return Ok(result);
            }
            catch (InvalidTokenException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (ServiceException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (PaginaXPacienteException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet]
        [Route("BuscarPaciente/{idPaciente:int}/{token}")]
        public async Task<IActionResult> GetPacienteIdAsync(string token, int idPaciente)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = _domain.GetByPacienteId(idPaciente);

                return Ok(result);
            }
            catch (InvalidTokenException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (ServiceException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (PaginaXPacienteException e)
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
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WpMedicos.Domains;
using WpMedicos.Entities;
using WpMedicos.Infrastructure.Exceptions;
using WpNoticias.Services;

namespace WpMedicos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoXPacienteController : ControllerBase
    {
        private readonly MedicoXPacienteDomain _domain;
        private readonly SegurancaService _service;

        public MedicoXPacienteController([FromServices]MedicoXPacienteDomain domain, [FromServices]SegurancaService service)
        {
            _domain = domain;
            _service = service;
        }

        [HttpPost("{token}")]
        public async Task<IActionResult> SaveAsync([FromRoute]string token, [FromBody]MedicoXPaciente medico)
        {
            try
            {
                await _service.ValidateTokenAsync(token);
                var result = _domain.Save(medico);

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
            catch (MedicoXPacienteException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet("{idCliente:int}/{token}")]
        public async Task<IActionResult> GetAsync([FromRoute]int idCliente, [FromRoute]string token)
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
            catch (MedicoXPacienteException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet("BuscarPorMedico/{idCliente:int}/{idMedico:int}/{token}")]
        public async Task<IActionResult> GetByIdMedicoAsync([FromRoute]int idCliente,[FromRoute]int idMedico ,[FromRoute]string token)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = _domain.GetAll(idCliente).Where(m => m.MedicoId == idMedico);
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
            catch (MedicoXPacienteException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet("BuscarPorPaciente/{idCliente:int}/{idPaciente:int}/{token}")]
        public async Task<IActionResult> GetByIdPacienteAsync([FromRoute]int idCliente, [FromRoute]int idPaciente, [FromRoute]string token)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = _domain.GetAll(idCliente).Where(m => m.IdPaciente == idPaciente);
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
            catch (MedicoXPacienteException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet("{idCliente:int}/{vinculoId:int}/{token}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute]int idCliente, [FromRoute]int vinculoId, [FromRoute]string token)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = _domain.GetById(vinculoId, idCliente);
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
            catch (MedicoXPacienteException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpPost("{idCliente:int}/{token}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]int idCliente, [FromRoute]string token, [FromBody]MedicoXPaciente medico)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = _domain.GetById(medico.ID, idCliente);
                _domain.Delete(result);

                return Ok(true);
            }
            catch (ServiceException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (InvalidTokenException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (MedicoXPacienteException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpPost("Update/{token}")]
        public async Task<IActionResult> UpdateAsync([FromRoute]string token, [FromBody]MedicoXPaciente medico)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = _domain.Update(medico);
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
            catch (MedicoXPacienteException e)
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

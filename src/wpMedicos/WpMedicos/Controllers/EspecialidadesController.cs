using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WpMedicos.Domains;
using WpMedicos.Entities;
using WpMedicos.Infrastructure.Exceptions;
using WpNoticias.Services;

namespace WpMedicos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadesController : ControllerBase
    {
        private readonly EspecialidadeDomain _domain;
        private readonly SegurancaService _service;

        public EspecialidadesController([FromServices]EspecialidadeDomain domain, [FromServices]SegurancaService service)
        {
            _domain = domain;
            _service = service;
        }

        [HttpPost("{token}")]
        public async Task<IActionResult> SaveAsync([FromRoute]string token, [FromBody]Especialidade especialidade)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = _domain.Save(especialidade);
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
            catch (EspecialidadeException e)
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
            catch (EspecialidadeException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet("{idCliente:int}/{especialidadeId:int}/{token}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute]int especialidadeId, [FromRoute]int idCliente, [FromRoute]string token)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = _domain.GetById(especialidadeId, idCliente);
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
            catch (EspecialidadeException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpPost("{idCliente:int}/{token}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]int idCliente, [FromRoute]string token, [FromBody]Especialidade especialidade)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = _domain.GetById(especialidade.ID, idCliente);
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
            catch (EspecialidadeException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpPost("BuscarEspcPorIds/{idCliente:int}/{token}")]
        public async Task<IActionResult> BuscarEspcPorIdsAsync([FromRoute]int idCliente, [FromRoute]string token, [FromBody]IEnumerable<int> ids)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = _domain.GetByIds(ids);

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
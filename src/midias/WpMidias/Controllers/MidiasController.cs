using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WpMidias.Domains;
using WpMidias.Domains.Helpers.Exceptions;
using WpMidias.Entities;
using WpMidias.Infrastructure.Exceptions;
using WpMidias.Services;

namespace WpMidias.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class MidiasController : ControllerBase
    {
        private readonly MidiasDomain _domain;
        private readonly SegurancaService _service;

        public MidiasController([FromServices]MidiasDomain domain, [FromServices]SegurancaService service)
        {
            _domain = domain;
            _service = service;
        }

        [HttpPost("{token}")]
        public async Task<IActionResult> SaveAsync([FromRoute]string token, [FromBody]Midia midia)
        {
            try
            {
                await _service.ValidateTokenAsync(token);
                var result = _domain.Save(midia);

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
            catch (FileSystemException e)
            {
                return StatusCode(502, e.Message);
            }
            catch (MidiaException e)
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

                var result = await _domain.GetAllAsync(idCliente);
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
            catch (FileSystemException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (MidiaException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet("{idCliente:int}/{midiaId:int}/{token}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute]int midiaId, [FromRoute]int idCliente, [FromRoute]string token)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = await _domain.GetByIdAsync(midiaId, idCliente);
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
            catch (MidiaException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (FileSystemException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpPost("{idCliente:int}/{token}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]int idCliente, [FromRoute]string token, [FromBody]Midia midia)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = await _domain.GetByIdAsync(midia.ID, idCliente);
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
            catch (MidiaException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (FileSystemException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet("GetByIdExterno/{idCliente:int}/{codigoExterno:int}/{token}")]
        public async Task<IActionResult> GetByCodigoExternoAsync([FromRoute]int codigoExterno, [FromRoute]int idCliente, [FromRoute]string token)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = await _domain.GetByCodigoExternoAsync(codigoExterno, idCliente);
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
            catch (MidiaException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (FileSystemException e)
            {
                return StatusCode(400, e.Message + e.InnerException.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpPost("GetByCodigosExternos/{idCliente:int}/{token}")]
        public async Task<IActionResult> GetByCodigosExternosAsync([FromRoute]IEnumerable<int> codigos, [FromRoute]int idCliente, [FromRoute]string token)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = await _domain.GetByCodigosExternosAsync(codigos, idCliente);
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
            catch (MidiaException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (FileSystemException e)
            {
                return StatusCode(400, e.Message + e.InnerException.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpPost("GetByCodExternoFromTake/{idCliente:int}/{codigoExterno:int}/{lastid:int}/{take:int}/{token}")]
        public async Task<IActionResult> GetByCodExternoFromTake([FromRoute]int codigoExterno, [FromRoute]int idCliente, [FromRoute] int lastid, [FromRoute] int take, [FromRoute]string token)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = await _domain.GetByCodExternoFromTake(codigoExterno, idCliente, lastid, take);
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
            catch (MidiaException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (FileSystemException e)
            {
                return StatusCode(400, e.Message + e.InnerException.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }
    }
}
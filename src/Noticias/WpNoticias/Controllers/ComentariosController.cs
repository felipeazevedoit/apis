using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WpNoticias.Domains;
using WpNoticias.Entities;
using WpNoticias.Infrastructure.Exceptions;
using WpNoticias.Services;

namespace WpNoticias.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentariosController : ControllerBase
    {
        private readonly SegurancaService _service;
        private readonly ComentariosDomain _domain;

        public ComentariosController([FromServices]SegurancaService service, [FromServices]ComentariosDomain domain)
        {
            _service = service;
            _domain = domain;
        }

        [HttpPost("{token}")]
        public async Task<IActionResult> SaveAsync([FromRoute]string token, [FromBody]Comentario comentario)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = _domain.Save(comentario);
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
            catch (ComentarioException e)
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
            catch (ComentarioException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet("{noticiaId:int}/{idCliente:int}/{token}")]
        public async Task<IActionResult> GetByNoticiaIdAsync([FromRoute]int noticiaId, [FromRoute]int idCliente, [FromRoute]string token)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = _domain.GetByNoticiaId(noticiaId, idCliente);
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
            catch (ComentarioException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet("GetById/{comentarioId:int}/{idCliente:int}/{token}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute]int comentarioId, [FromRoute]int idCliente, [FromRoute]string token)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = _domain.GetById(comentarioId, idCliente);
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
            catch (ComentarioException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpPost("{idCliente:int}/{token}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]int idCliente, [FromRoute]string token, [FromBody]Comentario comentario)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = _domain.GetById(comentario.ID, idCliente);
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
            catch (ComentarioException e)
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

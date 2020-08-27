using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WpNoticias.Domains;
using WpNoticias.Entities;
using WpNoticias.Infrastructure.Exceptions;
using WpNoticias.Services;

namespace WpNoticias.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoticiasController : ControllerBase
    {
        private readonly NoticiasDomain _domain;
        private readonly SegurancaService _service;

        public NoticiasController([FromServices]NoticiasDomain domain, [FromServices]SegurancaService service)
        {
            _domain = domain;
            _service = service;
        }

        [HttpPost("{token}")]
        public async Task<IActionResult> SaveAsync([FromRoute]string token, [FromBody]Noticia noticia)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = _domain.Save(noticia);
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
            catch (NoticiaException e)
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
            catch (NoticiaException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet("{idCliente:int}/{noticiaId:int}/{token}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute]int idCliente, [FromRoute]int noticiaId, [FromRoute]string token)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = _domain.GetById(noticiaId, idCliente);
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
            catch (NoticiaException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpPost("{idCliente:int}/{token}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]int idCliente, [FromRoute]string token, [FromBody]Noticia noticia)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = _domain.GetById(noticia.ID, idCliente);
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
            catch (NoticiaException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpPost("Publicar/{idCliente:int}/{token}")]
        public async Task<IActionResult> PublicarAsync([FromRoute]int idCliente, [FromRoute]string token, [FromBody]Noticia noticia)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = _domain.GetById(noticia.ID, idCliente);
                _domain.Publicar(result);

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
            catch (NoticiaException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpPost("Update/{token}")]
        public async Task<IActionResult> UpdateAsync([FromRoute]string token, [FromBody]Noticia noticia)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = _domain.Update(noticia);
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
            catch (NoticiaException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpPost("BuscarPorTags/{idCliente:int}/{codigoExterno:int}/{token}")]
        public async Task<IActionResult> BuscarPorTagsAsync([FromRoute]int idCliente, 
            [FromRoute]int codigoExterno, [FromRoute]string token, [FromBody]IEnumerable<string> tags)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = _domain.GetByTags(idCliente, codigoExterno, tags);
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
            catch (NoticiaException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet("BuscarPublicas/{idCliente:int}/{token}")]
        public async Task<IActionResult> BuscarPublicasAsync([FromRoute]int idCliente, [FromRoute]string token)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = _domain.GetNoticiasPublicas(idCliente);
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
            catch (NoticiaException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet("BuscarPorCodigoExterno/{idCliente:int}/{codigoExterno:int}/{token}")]
        public async Task<IActionResult> BuscarPorCodigoExternoAsync([FromRoute]int idCliente, [FromRoute]int codigoExterno, [FromRoute]string token)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = _domain.GetByCodigoExterno(idCliente, codigoExterno);
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
            catch (NoticiaException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet("BuscarPorCategoria/{idCliente:int}/{categoriaId:int}/{token}")]
        public async Task<IActionResult> BuscarPorCategoriaAsync([FromRoute]int idCliente, [FromRoute]int categoriaId, [FromRoute]string token)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = _domain.GetByCategoria(idCliente, categoriaId);
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
            catch (NoticiaException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet("BuscarPrivadas/{idCliente:int}/{token}")]
        public async Task<IActionResult> BuscarPrivadasAsync([FromRoute]int idCliente, [FromRoute]string token)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = _domain.GetNoticiasPrivadas(idCliente);
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
            catch (NoticiaException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet("Pesquisar/{idCliente:int}/{texto}/{token}")]
        public async Task<IActionResult> PesquisarAsync([FromRoute]int idCliente, [FromRoute]string token, [FromRoute]string texto)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var noticias = _domain.Pesquisar(idCliente, texto);

                return Ok(noticias);
            }
            catch (ServiceException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (InvalidTokenException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (NoticiaException e)
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
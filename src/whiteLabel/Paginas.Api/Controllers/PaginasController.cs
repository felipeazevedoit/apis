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
    public class PaginasController : ControllerBase
    {
        private readonly SegurancaService _service;
        private readonly PaginasDomain _domain;

        public PaginasController()
        {
            _service = new SegurancaService();
            _domain = new PaginasDomain(new PaginasRepository());
        }

        [HttpPost]
        [Route("{token}")]
        public async Task<IActionResult> SaveAsync(string token, [FromBody]Pagina pagina)
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
            catch (PaginasException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpPost]
        [Route("Desativar/{token}")]
        public async Task<IActionResult> RemoveAsync(string token, [FromBody]Pagina pagina)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var pg = _domain.Delete(pagina);

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
            catch (PaginasException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet]
        [Route("{idCliente}/{token}")]
        public async Task<IActionResult> GetAsync(string token, int idCliente)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = _domain.GetAll(idCliente);

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
            catch (PaginasException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet]
        [Route("{idCliente:int}/{paginaId:int}/{token}")]
        public async Task<IActionResult> GetByIdAsync(string token, int idCliente, int paginaId)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = _domain.GetById(paginaId, idCliente);

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
            catch (PaginasException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet]
        [Route("BuscarCodigos/{idCliente:int}/{token}")]
        public async Task<IActionResult> GetCodigosAsync(string token, int idCliente)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = _domain.GetAllCodigos(idCliente);

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
            catch (PaginasException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet]
        [Route("BuscarPorCodigoExterno/{idCliente:int}/{codigoExterno:int}/{token}")]
        public async Task<IActionResult> GetByCodigoExternoAsync(string token, int idCliente, int codigoExterno)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = _domain.GetByCodigoExterno(codigoExterno, idCliente);

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
            catch (PaginasException e)
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
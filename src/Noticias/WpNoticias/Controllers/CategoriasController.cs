using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WpNoticias.Domains;
using WpNoticias.Infrastructure.Exceptions;
using WpNoticias.Services;

namespace WpNoticias.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly CategoriasDomain _domain;
        private readonly SegurancaService _service;

        public CategoriasController([FromServices]CategoriasDomain domain, [FromServices]SegurancaService service)
        {
            _domain = domain;
            _service = service;
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
            catch (CategoriaException e)
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
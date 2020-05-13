using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WpFinanceiro.Domains;
using WpFinanceiro.Entities;
using WpFinanceiro.Infrastructure.Exceptions;
using WpFinanceiro.Services;

namespace WpFinanceiro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DadosBancariosController : ControllerBase
    {
        private readonly SegurancaService _service;
        private readonly DadosBancariosDomain _domain;

        public DadosBancariosController(SegurancaService service, DadosBancariosDomain domain)
        {
            _service = service;
            _domain = domain;
        }

        [HttpGet("{token}")]
        public async Task<IActionResult> GetAllAsync([FromRoute]string token)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = _domain.GetAll();
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
            catch (DadosBancariosException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet("{idCliente:int}/{cpf}/{token}")]
        public async Task<IActionResult> GetByCpfAsync([FromRoute]int idCliente, [FromRoute]string cpf, [FromRoute]string token)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = _domain.GetByCpf(cpf, idCliente);
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
            catch (DadosBancariosException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpPost("{token}")]
        public async Task<IActionResult> SaveAsync([FromRoute]string token, [FromBody]DadosBancarios conta)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = _domain.Save(conta);
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
            catch (DadosBancariosException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpPost("Alterar/{token}")]
        public async Task<IActionResult> UpdateAsync([FromRoute]string token, [FromBody]DadosBancarios conta)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                _domain.Update(conta);
                return Ok("Conta atualizada com sucesso.");
            }
            catch (ServiceException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (InvalidTokenException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (DadosBancariosException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet("{idCliente:int}/{codigoExterno:int}/{token}")]
        public async Task<IActionResult> GetByCodigoExternoAsync([FromRoute]int idCliente, [FromRoute]int codigoExterno, [FromRoute]string token)
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
            catch (DadosBancariosException e)
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
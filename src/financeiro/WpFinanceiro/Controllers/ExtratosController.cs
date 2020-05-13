using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WpFinanceiro.Domains;
using WpFinanceiro.Entities;
using WpFinanceiro.Helpers;
using WpFinanceiro.Infrastructure.Exceptions;
using WpFinanceiro.Services;

namespace WpFinanceiro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExtratosController : ControllerBase
    {
        private readonly SegurancaService _service;
        private readonly ExtratoDomain _domain;
        private readonly EmailHandler _emailHandler;

        public ExtratosController(SegurancaService service, ExtratoDomain domain, EmailHandler emailHandler)
        {
            _service = service;
            _domain = domain;
            _emailHandler = emailHandler;
        }

        [HttpPost("InserirCredito/{token}")]
        public async Task<IActionResult> InserirCreditoAsync([FromRoute]string token, [FromBody]IEnumerable<Extrato> extratos)
        {
            try
            {
                await _service.ValidateTokenAsync(token);
                var result = _domain.Inserir(extratos);

                var mainExtrato = extratos.FirstOrDefault();

                await _emailHandler.EnviaEmailAsync(token, mainExtrato);

                return Ok(result);
            }
            catch(ServiceException e)
            {
                return StatusCode(401, e.Message);
            }
            catch(InvalidTokenException e)
            {
                return StatusCode(401, e.Message);
            }
            catch(ExtratoException e)
            {
                return StatusCode(400, e.Message);
            }
            catch(Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpPost("Get/{token}")]
        public async Task<IActionResult> GetAsync([FromRoute]string token, [FromBody]Extrato extrato)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                IEnumerable<Extrato> extratos = default(List<Extrato>);
                if (extrato.DataInicio != null && extrato.DataInicio > DateTime.MinValue
                    && extrato.DataFim != null && extrato.DataFim > DateTime.MinValue)
                {
                    extratos = _domain.GetByRangeOfDate(extrato.DataInicio, extrato.DataFim);
                    return Ok(extratos);
                }

                extratos = await _domain.GetByProperties(extrato.GetProperties());
                return Ok(extratos);
            }
            catch(Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpPost("AlocarCredito/{token}")]
        public async Task<IActionResult> AlocarCreditoAsync([FromRoute]string token, [FromBody]IEnumerable<Extrato> extratos)
        {
            try
            {
                await _service.ValidateTokenAsync(token);
                var result = _domain.Alocar(extratos);

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
            catch (ExtratoException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet("LiberarPagamento/{idCliente:int}/{codigoExterno:int}/{destino}/{tipoDestino:int}/{token}")]
        public async Task<IActionResult> LiberarPagamentoAsync([FromRoute]string token,
            [FromRoute]int idCliente, [FromRoute]int codigoExterno, [FromRoute]string destino, [FromRoute]int tipoDestino)
        {
            try
            {
                await _service.ValidateTokenAsync(token);
                if(_domain.LiberarPagamento(idCliente, codigoExterno, destino, tipoDestino))
                    return Ok("Status Atualizado com sucesso!");

                return Ok("Não foi possível atualizar o Status.");
            }
            catch (ServiceException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (InvalidTokenException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (ExtratoException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet("BuscarSaldo/{idCliente:int}/{destino}/{tipoDestino:int}/{token}")]
        public async Task<IActionResult> BuscarSaldoAsync([FromRoute]int idCliente, 
            [FromRoute]string destino, [FromRoute]int tipoDestino, [FromRoute]string token)
        {
            try
            {
                await _service.ValidateTokenAsync(token);
                var saldo = _domain.GetSaldo(idCliente, destino, tipoDestino);

                return Ok(saldo);
            }
            catch (ServiceException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (InvalidTokenException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (ExtratoException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet("BuscarSaldoAprovado/{idCliente:int}/{destino}/{tipoDestino:int}/{token}")]
        public async Task<IActionResult> BuscarSaldoAprovadoAsync([FromRoute]int idCliente,
            [FromRoute]string destino, [FromRoute]int tipoDestino, [FromRoute]string token)
        {
            try
            {
                await _service.ValidateTokenAsync(token);
                var saldo = _domain.GetSaldoAprovado(idCliente, destino, tipoDestino);

                return Ok(saldo);
            }
            catch (ServiceException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (InvalidTokenException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (ExtratoException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet("BuscarSaldoPendente/{idCliente:int}/{destino}/{tipoDestino:int}/{token}")]
        public async Task<IActionResult> BuscarSaldoPendenteAsync([FromRoute]int idCliente,
            [FromRoute]string destino, [FromRoute]int tipoDestino, [FromRoute]string token)
        {
            try
            {
                await _service.ValidateTokenAsync(token);
                var saldo = _domain.GetSaldoPendente(idCliente, destino, tipoDestino);

                return Ok(saldo);
            }
            catch (ServiceException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (InvalidTokenException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (ExtratoException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet("BuscarExtratos/{idCliente:int}/{destino}/{tipoDestino:int}/{token}")]
        public async Task<IActionResult> BuscarExtratosAsync([FromRoute]int idCliente,
            [FromRoute]string destino, [FromRoute]int tipoDestino, [FromRoute]string token)
        {
            try
            {
                await _service.ValidateTokenAsync(token);
                var saldo = _domain.GetSByDestino(idCliente, destino, tipoDestino);

                return Ok(saldo);
            }
            catch (ServiceException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (InvalidTokenException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (ExtratoException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet("BuscarNaturezas/{token}")]
        public async Task<IActionResult> GetNaturezas([FromRoute]string token)
        {
            try
            {
                await _service.ValidateTokenAsync(token);
                var naturezas = _domain.GetNaturezas();

                return Ok(naturezas);
            }
            catch (ServiceException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (InvalidTokenException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (ExtratoException e)
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
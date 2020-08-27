using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WpPacientes.Domains;
using WpPacientes.Entities;
using WpPacientes.Infrastructure.Exceptions;
using WpPacientes.Services;

namespace WpPacientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private readonly PacientesDomain _domain;
        private readonly SegurancaService _service;
        private readonly EnderecoDomain _edDomain;
        private readonly TelefoneDomain _tDomain;
        private readonly PacientesXGruposDomain _pXgDomain;


        public PacientesController([FromServices]PacientesDomain domain, [FromServices]SegurancaService service, 
            [FromServices]EnderecoDomain edDomain, [FromServices]TelefoneDomain tDomain, [FromServices]PacientesXGruposDomain pXgDomain)
        {
            _domain = domain;
            _service = service;
            _edDomain = edDomain;
            _tDomain = tDomain;
            _pXgDomain = pXgDomain;
        }

        [HttpPost("{token}")]
        public async Task<IActionResult> SaveAsync([FromRoute]string token, [FromBody]Paciente paciente)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = _domain.Save(paciente);

                result.Endereco.PacienteId = result.ID;
                result.Endereco.UsuarioCriacao = result.CodigoExterno;
                result.Endereco.UsuarioEdicao = result.CodigoExterno;
                result.Endereco.IdUsuario = result.CodigoExterno;
                result.Endereco.IdCliente = result.IdCliente;
                result.Endereco.Status = 1;
                result.Endereco = await _edDomain.SaveAsync(result.Endereco, token);

                result.Telefone.PacienteId = result.ID;
                result.Telefone.Nome = result.Nome;
                result.Telefone.UsuarioCriacao = result.CodigoExterno;
                result.Telefone.UsuarioEdicao = result.CodigoExterno;
                result.Telefone.IdCliente = result.IdCliente;
                result.Telefone.Status = 1;
                result.Telefone = await _tDomain.SaveAsync(result.Telefone, token);

                return Ok(result);
            }
            catch(InvalidTokenException e)
            {
                return StatusCode(401, e.Message);
            }
            catch(ServiceException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (PacienteException e)
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
                var enderecos = _edDomain.GetAlls(result.Select(m => m.ID), token);
                var telefone = _tDomain.GetAlls(result.Select(m => m.ID), token);


                for (int i = 0; i < result.Count(); i++)
                {
                    result.ElementAtOrDefault(i).Endereco = enderecos.FirstOrDefault(e => e.PacienteId.Equals(result.ElementAtOrDefault(i).ID));
                    result.ElementAtOrDefault(i).Telefone = telefone.FirstOrDefault(e => e.PacienteId.Equals(result.ElementAtOrDefault(i).ID));
                }

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
            catch (PacienteException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet("{idCliente:int}/{pacienteId:int}/{token}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute]int pacienteId, [FromRoute]int idCliente, [FromRoute]string token)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = _domain.GetById(pacienteId, idCliente);
                var endereco = await _edDomain.GetByPacienteIdAsync(pacienteId, idCliente, token);
                var telefone = await _tDomain.GetByPacienteIdAsync(pacienteId, idCliente, token);


                result.Endereco = endereco;
                result.Telefone = telefone;

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
            catch (PacienteException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpPost("GetByIds/{idCliente:int}/{token}")]
        public async Task<IActionResult> GetByIdsAsync([FromBody]IEnumerable<int> pacientesIds, [FromRoute]int idCliente, [FromRoute]string token)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = _domain.GetByIds(pacientesIds, idCliente);
                var enderecos = _edDomain.GetAlls(result.Select(m => m.ID), token);
                var telefones = _tDomain.GetAlls(result.Select(m => m.ID), token);

                for (int i = 0; i < result.Count(); i++)
                {
                    result.ElementAtOrDefault(i).Endereco = enderecos.FirstOrDefault(e => e.PacienteId.Equals(result.ElementAtOrDefault(i).ID));
                    result.ElementAtOrDefault(i).Telefone = telefones.FirstOrDefault(e => e.PacienteId.Equals(result.ElementAtOrDefault(i).ID));
                }
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
            catch (PacienteException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpPost("{idCliente:int}/{token}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]int idCliente, [FromRoute]string token, [FromBody]Paciente paciente)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = _domain.GetById(paciente.ID, idCliente);
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
            catch (PacienteException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet("GetByIdExterno/{idCliente:int}/{codigoExterno:int}/{token}")]
        public async Task<IActionResult> GetByIdExternoAsync([FromRoute]int codigoExterno, [FromRoute]int idCliente, [FromRoute]string token)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = _domain.GetByIdExterno(codigoExterno, idCliente);
                var endereco = await _edDomain.GetByCodExternoAsync(codigoExterno, idCliente, token);
                var telefone = await _tDomain.GetByCodExternoAsync(codigoExterno, idCliente, token);

                result.Endereco = endereco;
                result.Telefone = telefone;
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
            catch (PacienteException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpPost("SalvarPacientesXGrupos/{token}")]
        public async Task<IActionResult> SalvarPacientesXGruposAsync([FromBody]IEnumerable<PacientesXGrupos> pacientesXGrupos, [FromRoute]string token)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                _pXgDomain.Save(pacientesXGrupos);

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
            catch (PacienteException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet("BuscarPacientesPorGrupo/{idcliente:int}/{grupoId}/{token}")]
        public async Task<IActionResult> BuscarPacientesPorGrupoAsync([FromRoute]int idCliente, [FromRoute]int grupoId, [FromRoute]string token)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var x = _pXgDomain.GetByGrupo(grupoId);

                IEnumerable<Paciente> pacientes = null;

                if (x != null)
                {
                    pacientes = _domain.GetByIds(x.Select(o => o.PacienteId), idCliente);

                }
                return Ok(pacientes);
            }
            catch (ServiceException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (InvalidTokenException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (PacienteException e)
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
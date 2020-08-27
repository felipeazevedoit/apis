using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
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
    public class MedicosController : ControllerBase
    {
        private readonly MedicoDomain _domain;
        private readonly EspecialidadeDomain _eDomain;
        private readonly MedicosXEspecialidadesDomain _mXeDomain;
        private readonly SegurancaService _service;
        private readonly EnderecoDomain _edDomain;
        private readonly TelefoneDomain _tDomain;
        private readonly MedicoXClinicasDomain _mXcDomain;

        public MedicosController([FromServices]MedicoDomain domain, [FromServices]SegurancaService service, [FromServices]EspecialidadeDomain eDomain,
            [FromServices] MedicosXEspecialidadesDomain mXeDoamin, [FromServices]EnderecoDomain edDomain, [FromServices]TelefoneDomain tDomain, MedicoXClinicasDomain mXcDomain)
        {
            _domain = domain;
            _service = service;
            _eDomain = eDomain;
            _mXeDomain = mXeDoamin;
            _edDomain = edDomain;
            _tDomain = tDomain;
            _mXcDomain = mXcDomain;
        }

        [HttpPost("{token}")]
        public async Task<IActionResult> SaveAsync([FromRoute]string token, [FromBody]Medico medico)
        {
            try
            {
                //await _service.ValidateTokenAsync(token);

                var result = _domain.Save(medico);

                var oldMXe = default(MedicoXEspecialidade);
                if (medico.ID > 0)
                {
                    oldMXe = _mXeDomain.GetByMedicoId(result.ID).FirstOrDefault();
                }

                var mXe = _mXeDomain.Save(new MedicoXEspecialidade(oldMXe == null ? 0 : oldMXe.ID, result.ID, result.EspecialidadeId));
                result.Especialidade = _eDomain.GetById(mXe.EspecialidadeId, result.IdCliente);

                result.Endereco.MedicoId = result.ID;
                result.Endereco.UsuarioCriacao = result.UsuarioCriacao;
                result.Endereco.UsuarioEdicao = result.UsuarioEdicao;
                result.Endereco.IdUsuario = result.IdUsuario;
                result.Endereco = await _edDomain.SaveAsync(result.Endereco, token);

                result.Telefone.MedicoId = result.ID;
                result.Telefone.Nome = result.Nome;
                result.Telefone.UsuarioCriacao = result.UsuarioCriacao;
                result.Telefone.UsuarioEdicao = result.UsuarioEdicao;
                result.Telefone = await _tDomain.SaveAsync(result.Telefone, token);

                var omXc = new MedicoXClinicas();
                if (medico.idsClinicas != null)
                {
                    omXc.MedicoId = result.ID;
                    var buscaMedico = _mXcDomain.GetByMedicoId(omXc.MedicoId);
                    if (buscaMedico.Count() > 0)
                    {
                        for (int i = 0; i < buscaMedico.Count(); i++)
                        {
                            omXc.ID = buscaMedico.ElementAtOrDefault(i).ID;
                            _mXcDomain.Delete(omXc);
                        }
                    }
                    foreach (var item in medico.idsClinicas)
                    {
                        omXc.ID = 0;
                        omXc.ClinicaId = item;

                        var mXc = _mXcDomain.Save(omXc);
                    }
                }

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
            catch (MedicosXEspecialidadesException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (MedicoException e)
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
                var enderecos = _edDomain.GetAlls(result.Select(m => m.Medico.ID), token);
                var telefone = _tDomain.GetAlls(result.Select(m => m.Medico.ID), token);

                for (int i = 0; i < result.Count(); i++)
                {
                    if (enderecos != null)
                    {
                        result.ElementAtOrDefault(i).Medico.Endereco = enderecos.FirstOrDefault(e => e.MedicoId.Equals(result.ElementAtOrDefault(i).Medico.ID));
                    }

                    if (telefone != null)
                    {
                        result.ElementAtOrDefault(i).Medico.Telefone = telefone.FirstOrDefault(e => e.MedicoId.Equals(result.ElementAtOrDefault(i).Medico.ID));
                    }
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
            catch (MedicoException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet("GetByEspecialidade/{idCliente:int}/{especialidadeId:int}/{token}")]
        public async Task<IActionResult> GetByEspecialidadeAsync([FromRoute]int idCliente, [FromRoute]int especialidadeId, [FromRoute]string token)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = await _domain.GetAllEspecialidadeAsync(especialidadeId, idCliente);
                var enderecos = _edDomain.GetAlls(result.Select(m => m.Medico.ID), token);
                var telefone = _tDomain.GetAlls(result.Select(m => m.Medico.ID), token);

                for (int i = 0; i < result.Count(); i++)
                {
                    result.ElementAtOrDefault(i).Medico.Endereco = enderecos.FirstOrDefault(e => e.MedicoId.Equals(result.ElementAtOrDefault(i).Medico.ID));
                    result.ElementAtOrDefault(i).Medico.Telefone = telefone.FirstOrDefault(e => e.MedicoId.Equals(result.ElementAtOrDefault(i).Medico.ID));
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
            catch (MedicoException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet("{idCliente:int}/{medicoId:int}/{token}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute]int medicoId, [FromRoute]int idCliente, [FromRoute]string token)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = await _domain.GetByIdAsync(medicoId, idCliente);
                var endereco = await _edDomain.GetByMedicoIdAsync(medicoId, idCliente, token);
                var telefone = await _tDomain.GetByMedicoIdAsync(medicoId, idCliente, token);

                result.Medico.Endereco = endereco;
                result.Medico.Telefone = telefone;

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
            catch (MedicoException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet("BuscarPorIdExterno/{idCliente:int}/{idExterno:int}/{token}")]
        public async Task<IActionResult> GetByIdExternoAsync([FromRoute]int idExterno, [FromRoute]int idCliente, [FromRoute]string token)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                if (idExterno > 0)
                {
                    var result = _domain.GetByIdExterno(idExterno, idCliente);

                    var endereco = await _edDomain.GetByCodExternoAsync(idExterno, idCliente, token);
                    var telefone = await _tDomain.GetByCodExternoAsync(idExterno, idCliente, token);

                    result.Endereco = endereco;
                    result.Telefone = telefone;

                    return Ok(result);
                }

                return Ok(null);
            }
            catch (ServiceException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (InvalidTokenException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (MedicoException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpPost("{idCliente:int}/{token}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]int idCliente, [FromRoute]string token, [FromBody]Medico medico)
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
            catch (MedicoException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpPost("BuscarPorCodigos/{idCliente:int}/{token}")]
        public async Task<IActionResult> BuscarPorCodigosAsync([FromRoute]int idCliente, [FromRoute]string token, [FromBody]IEnumerable<int> ids)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = _domain.GetByIds(ids, idCliente);
                var enderecos = _edDomain.GetAlls(result.Select(m => m.ID), token);
                var telefone = _tDomain.GetAlls(result.Select(m => m.ID), token);

                for (int i = 0; i < result.Count(); i++)
                {
                    result.ElementAtOrDefault(i).Endereco = enderecos.FirstOrDefault(e => e.MedicoId.Equals(result.ElementAtOrDefault(i).ID));
                    result.ElementAtOrDefault(i).Telefone = telefone.FirstOrDefault(e => e.MedicoId.Equals(result.ElementAtOrDefault(i).ID));
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
            catch (MedicoException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpPost("BuscarPorCodigosExternos/{idCliente:int}/{token}")]
        public async Task<IActionResult> BuscarPorCodigosExternosAsync([FromRoute]int idCliente, [FromRoute]string token, [FromBody]IEnumerable<int> ids)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var result = _domain.GetByIdsExternos(ids, idCliente);
                var enderecos = _edDomain.GetAlls(result.Select(m => m.ID), token);
                var telefone = _tDomain.GetAlls(result.Select(m => m.ID), token);

                for (int i = 0; i < result.Count(); i++)
                {
                    result.ElementAtOrDefault(i).Endereco = enderecos.FirstOrDefault(e => e.MedicoId.Equals(result.ElementAtOrDefault(i).ID));
                    result.ElementAtOrDefault(i).Telefone = telefone.FirstOrDefault(e => e.MedicoId.Equals(result.ElementAtOrDefault(i).ID));
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
            catch (MedicoException e)
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

                var medicos = _domain.Pesquisar(idCliente, texto);

                return Ok(medicos);
            }
            catch (ServiceException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (InvalidTokenException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (MedicoException e)
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
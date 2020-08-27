using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpMedicos.Domains.Generics;
using WpMedicos.Entities;
using WpMedicos.Infrastructure;
using WpMedicos.Infrastructure.Exceptions;

namespace WpMedicos.Domains
{
    public class MedicoDomain : IDomain<Medico>
    {
        private readonly MedicoRepository _repository;

        public MedicoDomain(MedicoRepository repository)
        {
            _repository = repository;
        }

        public void Delete(Medico entity)
        {
            try
            {
                entity.DataEdicao = DateTime.UtcNow;
                entity.Status = 9;
                entity.Ativo = false;

                _repository.Update(entity);
            }
            catch (Exception e)
            {
                throw new MedicoException("Não foi possível desativar o medico informado.", e);
            }
        }

        public IEnumerable<Medico> GetAll()
        {
            try
            {
                var result = _repository.GetAll();
                return result;
            }
            catch (Exception e)
            {
                throw new MedicoException("Não foi possível recuperar os medicos disponíveis.", e);
            }
        }

        public IEnumerable<Medico> GetAll(int idCliente)
        {
            try
            {
                var result = _repository.GetList(p => p.IdCliente.Equals(idCliente));
                return result;
            }
            catch (Exception e)
            {
                throw new MedicoException("Não foi possível recuperar os medicos solicitados.", e);
            }
        }

        public async Task<IEnumerable<MedicoXEspecialidade>> GetAllAsync(int idCliente)
        {
            try
            {
                var result = await _repository.GetAllWithEspecialidadesAsync(idCliente);
                return result;
            }
            catch (Exception e)
            {
                throw new MedicoException("Não foi possível recuperar os medicos solicitados.", e);
            }
        }

        public async Task<IEnumerable<MedicoXEspecialidade>> GetAllEspecialidadeAsync(int especialidadeId, int idCliente)
        {
            try
            {
                var result = await _repository.GetAllByEspecialidadeAsync(especialidadeId, idCliente);
                return result;
            }
            catch (Exception e)
            {
                throw new MedicoException("Não foi possível recuperar os medicos solicitados.", e);
            }
        }

        public Medico GetById(int entityId, int idCliente)
        {
            try
            {
                var result = _repository.GetSingle(p => p.ID.Equals(entityId) && p.IdCliente.Equals(idCliente));
                return result;
            }
            catch (Exception e)
            {
                throw new MedicoException("Não foi possível recuperar o medico solicitado.", e);
            }
        }

        public async Task<MedicoXEspecialidade> GetByIdAsync(int entityId, int idCliente)
        {
            try
            {
                var result = await _repository.GetWithEspecialidadeAsync(entityId, idCliente);
                return result;
            }
            catch (Exception e)
            {
                throw new MedicoException("Não foi possível recuperar o medico solicitado.", e);
            }
        }

        public Medico Save(Medico entity)
        {
            try
            {
                var paciente = default(Medico);
                switch (entity.ID)
                {
                    case 0:
                        entity.DataCriacao = DateTime.UtcNow;
                        entity.DataEdicao = DateTime.UtcNow;
                        //entity.Status = 9;
                        //entity.Ativo = false;

                        paciente = _repository.Add(entity).SingleOrDefault();
                        break;
                    default:
                        paciente = Update(entity);
                        break;
                }

                return paciente;
            }
            catch (MedicoException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new MedicoException("Não foi possível salvar o medico informado.", e);
            }
        }

        public Medico Update(Medico entity)
        {
            try
            {
                entity.DataEdicao = DateTime.UtcNow;
                _repository.Update(entity);

                return entity;
            }
            catch (Exception e)
            {
                throw new MedicoException("Não foi possível atualizar o medico informado.", e);
            }
        }

        public Medico GetByIdExterno(int idExterno, int idCliente)
        {
            try
            {
                var result = _repository.GetList(p => p.CodigoExterno.Equals(idExterno) && p.IdCliente.Equals(idCliente)).SingleOrDefault();
                return result;
            }
            catch(Exception e)
            {
                throw new MedicoException("Não foi possível buscar o medico solicitado.", e);
            }
        }

        public IEnumerable<Medico> GetByIds(IEnumerable<int> ids, int idCliente)
        {
            try
            {
                var result = _repository.GetList(p => ids.Contains(p.ID) && p.IdCliente.Equals(idCliente));
                return result;
            }
            catch (Exception e)
            {
                throw new MedicoException("Não foi possível buscar os medicos solicitados.", e);
            }
        }

        public IEnumerable<Medico> GetByIdsExternos(IEnumerable<int> ids, int idCliente)
        {
            try
            {
                var result = _repository.GetList(p => ids.Contains(p.CodigoExterno) && p.IdCliente.Equals(idCliente));
                return result;
            }
            catch (Exception e)
            {
                throw new MedicoException("Não foi possível buscar os medicos solicitados.", e);
            }
        }

        public IEnumerable<Medico> Pesquisar(int idCliente, string texto)
        {
            try
            {
                var result = _repository.GetList(p => p.Nome.ToUpper().Contains(texto.ToUpper()) && p.IdCliente.Equals(idCliente));
                return result;
            }
            catch (Exception e)
            {
                throw new MedicoException("Não foi possível buscar os medicos solicitados.", e);
            }
        }
    }
}
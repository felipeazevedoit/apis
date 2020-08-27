using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpMedicos.Domains.Generics;
using WpMedicos.Entities;
using WpMedicos.Infrastructure;
using WpMedicos.Infrastructure.Exceptions;

namespace WpMedicos.Domains
{
    public class EspecialidadeDomain : IDomain<Especialidade>
    {
        private readonly EspecialidadeRepository _repository;

        public EspecialidadeDomain(EspecialidadeRepository repository)
        {
            _repository = repository;
        }

        public void Delete(Especialidade entity)
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
                throw new EspecialidadeException("Não foi possível desativar a especialidade informada.", e);
            }
        }

        public IEnumerable<Especialidade> GetAll()
        {
            try
            {
                var result = _repository.GetAll();
                return result;
            }
            catch (Exception e)
            {
                throw new EspecialidadeException("Não foi possível recuperar as especialidades disponíveis.", e);
            }
        }

        public IEnumerable<Especialidade> GetAll(int idCliente)
        {
            try
            {
                var result = _repository.GetList(p => p.IdCliente.Equals(idCliente));
                return result;
            }
            catch (Exception e)
            {
                throw new EspecialidadeException("Não foi possível recuperar as especialidades solicitadas.", e);
            }
        }

        public Especialidade GetById(int entityId, int idCliente)
        {
            try
            {
                var result = _repository.GetSingle(p => p.ID.Equals(entityId) && p.IdCliente.Equals(idCliente));
                return result;
            }
            catch (Exception e)
            {
                throw new EspecialidadeException("Não foi possível recuperar a especialidade solicitada.", e);
            }
        }

        public Especialidade Save(Especialidade entity)
        {
            try
            {
                var paciente = default(Especialidade);
                switch (entity.ID)
                {
                    case 0:
                        entity.DataCriacao = DateTime.UtcNow;
                        entity.DataEdicao = DateTime.UtcNow;
                        entity.Status = 9;
                        entity.Ativo = false;

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
                throw new MedicoException("Não foi possível salvar a especialidade informada.", e);
            }
        }

        public Especialidade Update(Especialidade entity)
        {
            try
            {
                entity.DataEdicao = DateTime.UtcNow;
                _repository.Update(entity);

                return entity;
            }
            catch (Exception e)
            {
                throw new MedicoException("Não foi possível atualizar a especialidade informada.", e);
            }
        }

        public IEnumerable<Especialidade> GetByIds(IEnumerable<int> ids)
        {
            try
            {
                var result = _repository.GetList(e => ids.Contains(e.ID));
                return result;
            }
            catch (Exception e)
            {
                throw new MedicosXEspecialidadesException("Não foi possível recuperar as especialidades solicitadas.", e);
            }
        }
    }
}

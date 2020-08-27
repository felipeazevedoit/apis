using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpNoticias.Domains.Generics;
using WpNoticias.Entities;
using WpNoticias.Infrastructure;
using WpNoticias.Infrastructure.Exceptions;

namespace WpNoticias.Domains
{
    public class NoticiaXPacienteDomain : IDomain<NoticiaXPaciente>
    {
        private readonly NoticiaXPacienteRepository _repository;

        public NoticiaXPacienteDomain(NoticiaXPacienteRepository repository)
        {
            _repository = repository;
        }

        public void Delete(NoticiaXPaciente entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NoticiaXPaciente> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NoticiaXPaciente> GetAll(int idCliente)
        {
            throw new NotImplementedException();
        }

        public NoticiaXPaciente GetById(int entityId, int idCliente)
        {
            throw new NotImplementedException();
        }

        public NoticiaXPaciente Save(NoticiaXPaciente entity)
        {
            try
            {
                var nXp = default(NoticiaXPaciente);

                switch (entity.ID)
                {
                    case 0:
                        entity.DataVisualizacao = DateTime.UtcNow;
                        nXp = _repository.Add(entity).SingleOrDefault();
                        break;
                    default:
                        nXp = Update(entity);
                        break;
                }

                return nXp;
            }
            catch (Exception e)
            {
                throw new NoticiaXPacienteException("Não foi possível completar a operação.", e);
            }
        }

        public NoticiaXPaciente Update(NoticiaXPaciente entity)
        {
            try
            {
                _repository.Update(entity);

                return entity;
            }
            catch (Exception e)
            {
                throw new NoticiaXPacienteException("Não foi possível atualizar a categoria informada.", e);
            }
        }

        public IEnumerable<NoticiaXPaciente> GetByIdsPaciente(IEnumerable<int> pacienteIds)
        {
            try
            {
                var result = _repository.GetList(mXe => pacienteIds.Contains(mXe.PacienteId));
                return result;
            }
            catch (Exception e)
            {
                throw new NoticiaXPacienteException("Não foi possível recuperar os registros solicitados.", e);
            }
        }

        public IEnumerable<NoticiaXPaciente> GetByIdsNoticias(IEnumerable<int> noticiasIds)
        {
            try
            {
                var result = _repository.GetList(mXe => noticiasIds.Contains(mXe.NoticiaId));
                return result;
            }
            catch (Exception e)
            {
                throw new NoticiaXPacienteException("Não foi possível recuperar os registros solicitados.", e);
            }
        }

        public IEnumerable<NoticiaXPaciente> GetByNoticiaId(int noticiaId)
        {
            try
            {
                var result = _repository.GetList(mXe => mXe.NoticiaId.Equals(noticiaId));
                return result;
            }
            catch (Exception e)
            {
                throw new NoticiaXPacienteException("Não foi possível recuperar os registros solicitados.", e);
            }
        }

        public IEnumerable<NoticiaXPaciente> GetByPacienteId(int pacienteId)
        {
            try
            {
                var result = _repository.GetList(mXe => mXe.PacienteId.Equals(pacienteId));
                return result;
            }
            catch (Exception e)
            {
                throw new NoticiaXPacienteException("Não foi possível recuperar os registros solicitados.", e);
            }
        }
    }
}

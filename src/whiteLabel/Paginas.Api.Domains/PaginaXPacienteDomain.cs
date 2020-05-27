using Paginas.Api.Domains.Generics;
using Paginas.Api.Entities;
using Paginas.Api.Infrastructure;
using Paginas.Api.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paginas.Api.Domains
{
    public class PaginaXPacienteDomain : IDomain<PaginaXPaciente>
    {
        private readonly PaginaXPacienteRepository _repository;

        public PaginaXPacienteDomain(PaginaXPacienteRepository repository)
        {
            _repository = repository;
        }

        public PaginaXPaciente Delete(PaginaXPaciente entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PaginaXPaciente> GetAll(int idCliente)
        {
            throw new NotImplementedException();
        }

        public PaginaXPaciente GetById(int entityId, int idCliente)
        {
            throw new NotImplementedException();
        }

        public PaginaXPaciente Save(PaginaXPaciente entity)
        {
            try
            {
                var nXp = default(PaginaXPaciente);

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
                throw new PaginaXPacienteException("Não foi possível completar a operação.", e);
            }
        }

        public PaginaXPaciente Update(PaginaXPaciente entity)
        {
            try
            {
                _repository.Update(entity);

                return entity;
            }
            catch (Exception e)
            {
                throw new PaginaXPacienteException("Não foi possível atualizar a categoria informada.", e);
            }
        }

        public IEnumerable<PaginaXPaciente> GetByPaginaId(int paginaId)
        {
            try
            {
                var result = _repository.GetList(mXe => mXe.PaginaId.Equals(paginaId));
                return result;
            }
            catch (Exception e)
            {
                throw new PaginaXPacienteException("Não foi possível recuperar os registros solicitados.", e);
            }
        }

        public IEnumerable<PaginaXPaciente> GetByPacienteId(int pacienteId)
        {
            try
            {
                var result = _repository.GetList(mXe => mXe.PacienteId.Equals(pacienteId));
                return result;
            }
            catch (Exception e)
            {
                throw new PaginaXPacienteException("Não foi possível recuperar os registros solicitados.", e);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpPacientes.Domains.Generics;
using WpPacientes.Entities;
using WpPacientes.Infrastructure;
using WpPacientes.Infrastructure.Exceptions;

namespace WpPacientes.Domains
{
    public class PacientesXGruposDomain : IDomain<PacientesXGrupos>
    {
        private readonly PacientesXGruposRepository _repository;

        public PacientesXGruposDomain(PacientesXGruposRepository repository)
        {
            _repository = repository;
        }

        public void Delete(PacientesXGrupos entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PacientesXGrupos> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PacientesXGrupos> GetAll(int idCliente)
        {
            throw new NotImplementedException();
        }

        public PacientesXGrupos GetById(int entityId, int idCliente)
        {
            throw new NotImplementedException();
        }

        public PacientesXGrupos Save(PacientesXGrupos entity)
        {
            throw new NotImplementedException();
        }

        public void Save(IEnumerable<PacientesXGrupos> entities)
        {
            try
            {
                foreach (var entity in entities)
                {
                    var x = default(PacientesXGrupos);
                    switch (entity.ID)
                    {
                        case 0:
                            var r = _repository.GetSingle(o => o.GrupoId.Equals(entity.GrupoId) && o.PacienteId.Equals(entity.PacienteId));
                            if(r == null)
                                x = _repository.Add(entity).SingleOrDefault();
                            break;
                        default:
                            x = Update(entity);
                            break;
                    }
                }
            }
            catch (PacienteException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new PacienteException("Não foi possível salvar o dado informado.", e);
            }
        }

        public PacientesXGrupos Update(PacientesXGrupos entity)
        {
            try
            {
                _repository.Update(entity);

                return entity;
            }
            catch (Exception e)
            {
                throw new PacienteException("Não foi possível atualizar o dado informado.", e);
            }
        }

        public IEnumerable<PacientesXGrupos> GetByGrupo(int grupoId)
        {
            try
            {
                var result = _repository.GetList(x => x.GrupoId.Equals(grupoId));
                return result;
            }
            catch (Exception e)
            {
                throw new PacienteException("Não foi possível buscar os dados solicitados.", e);
            }
        }
    }
}

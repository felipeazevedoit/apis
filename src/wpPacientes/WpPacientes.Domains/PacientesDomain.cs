using System;
using System.Collections.Generic;
using System.Linq;
using WpPacientes.Domains.Generics;
using WpPacientes.Entities;
using WpPacientes.Infrastructure;
using WpPacientes.Infrastructure.Exceptions;

namespace WpPacientes.Domains
{
    public class PacientesDomain : IDomain<Paciente>
    {
        private readonly PacientesRepository _repository;

        public PacientesDomain(PacientesRepository repository)
        {
            _repository = repository;
        }

        public void Delete(Paciente entity)
        {
            try
            {
                entity.DataEdicao = DateTime.UtcNow;
                entity.Status = 9;
                entity.Ativo = false;

                _repository.Update(entity);
            }
            catch(Exception e)
            {
                throw new PacienteException("Não foi possível desativar o paciente informado.", e);
            }
        }

        public IEnumerable<Paciente> GetAll()
        {
            try
            {
                var result = _repository.GetAll();
                return result;
            }
            catch(Exception e)
            {
                throw new PacienteException("Não foi possível recuperar os pacientes disponíveis.",e);
            }
        }

        public IEnumerable<Paciente> GetAll(int idCliente)
        {
            try
            {
                var result = _repository.GetList(p => p.IdCliente.Equals(idCliente));
                return result;
            }
            catch(Exception e)
            {
                throw new PacienteException("Não foi possível recuperar os pacientes solicitados.", e);
            }
        }

        public Paciente GetById(int entityId, int idCliente)
        {
            try
            {
                var result = _repository.GetSingle(p => p.ID.Equals(entityId) && p.IdCliente.Equals(idCliente));
                return result;
            }
            catch(Exception e)
            {
                throw new PacienteException("Não foi possível recuperar o paciente solicitado.", e);
            }
        }

        public IEnumerable<Paciente> GetByIds(IEnumerable<int> ids, int idCliente)
        {
            try
            {
                var result = _repository.GetList(p => ids.Contains(p.ID) && p.IdCliente.Equals(idCliente));
                return result;
            }
            catch (Exception e)
            {
                throw new PacienteException("Não foi possível recuperar o paciente solicitado.", e);
            }
        }

        public Paciente GetByIdExterno(int codigoExterno, int idCliente)
        {
            try
            {
                var result = _repository.GetSingle(p => p.CodigoExterno.Equals(codigoExterno) && p.IdCliente.Equals(idCliente));
                return result;
            }
            catch (Exception e)
            {
                throw new PacienteException("Não foi possível recuperar o paciente solicitado.", e);
            }
        }

        public Paciente Save(Paciente entity)
        {
            try
            {
                var paciente = default(Paciente);
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
            catch(PacienteException e)
            {
                throw e;
            }
            catch(Exception e)
            {
                throw new PacienteException("Não foi possível salvar o paciente informado.", e);
            }
        }

        public Paciente Update(Paciente entity)
        {
            try
            {
                entity.DataEdicao = DateTime.UtcNow;
                _repository.Update(entity);

                return entity;
            }
            catch(Exception e)
            {
                throw new PacienteException("Não foi possível atualizar o paciente informado.", e);
            }
        }
    }
}

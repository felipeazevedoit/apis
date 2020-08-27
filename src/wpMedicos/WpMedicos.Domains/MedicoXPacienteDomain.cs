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
    public class MedicoXPacienteDomain : IDomain<MedicoXPaciente>
    {
        private readonly MedicoXPacienteRepository _repository;

        public MedicoXPacienteDomain(MedicoXPacienteRepository repository)
        {
            _repository = repository;
        }

        public void Delete(MedicoXPaciente entity)
        {
            try
            {
                entity.DataEdicao = DateTime.UtcNow;
                entity.Ativo = false;
                entity.Status = 9;

                _repository.Update(entity);
            }
            catch (Exception e)
            {
                throw new MedicoXPacienteException("Não foi possível desativar a associacao informado.", e);
            }
        }

        public IEnumerable<MedicoXPaciente> GetAll()
        {
            try
            {
                var result = _repository.GetAll().OrderByDescending(n => n.DataCriacao).ToList();
                return result;
            }
            catch (Exception e)
            {
                throw new MedicoXPacienteException("Não foi possível listar os convites.", e);
            }
        }

        public IEnumerable<MedicoXPaciente> GetAll(int idCliente)
        {
            try
            {
                var result = _repository.GetList(n => n.IdCliente.Equals(idCliente)).OrderByDescending(n => n.DataCriacao).ToList();
                return result;
            }
            catch (Exception e)
            {
                throw new MedicoXPacienteException("Não foi possível listar os convites solicitadas.", e);
            }
        }

        public MedicoXPaciente GetById(int entityId, int idCliente)
        {
            try
            {
                var result = _repository.GetSingle(n => n.ID.Equals(entityId) && n.IdCliente.Equals(idCliente));
                return result;
            }
            catch (Exception e)
            {
                throw new MedicoXPacienteException("Não foi possível recuperar o convite solicitado.", e);
            }
        }

        public MedicoXPaciente Save(MedicoXPaciente entity)
        {
            try
            {
                var mXp = default(MedicoXPaciente);
                switch (entity.ID)
                {
                    case 0:
                        entity.DataCriacao = DateTime.UtcNow;
                        entity.DataEdicao = DateTime.UtcNow;
                        //entity.Ativo = false;
                        //entity.Status = 9;
                        mXp = _repository.Add(entity).SingleOrDefault();
                        break;
                    default:
                        mXp = Update(entity);
                        break;
                }

                return mXp;
            }
            catch (MedicoXPacienteException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new MedicoXPacienteException("Não foi possível atualizar o registro informado.", e);
            }
        }

        public MedicoXPaciente Update(MedicoXPaciente entity)
        {
            try
            {
                _repository.Update(entity);
                return entity;
            }
            catch (Exception e)
            {
                throw new MedicosXEspecialidadesException("Não foi possível atualizar o registro informado.", e);
            }
        }
    }
}

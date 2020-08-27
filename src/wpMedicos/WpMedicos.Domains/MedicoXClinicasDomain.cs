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
    public class MedicoXClinicasDomain : IDomain<MedicoXClinicas>
    {
        private readonly MedicoXClinicasRepository _repository;
        public MedicoXClinicasDomain(MedicoXClinicasRepository repository)
        {
            _repository = repository;
        }
        public void Delete(MedicoXClinicas entity)
        {
            try
            {
                _repository.Remove(entity);
                //return result;
            }
            catch (Exception e)
            {
                throw new MedicoXClinicasException("Não foi possível recuperar os medicos disponíveis.", e);
            }
        }
        public IEnumerable<MedicoXClinicas> GetAll()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<MedicoXClinicas> GetAll(int idCliente)
        {
            throw new NotImplementedException();
        }
        public MedicoXClinicas GetById(int entityId, int idCliente)
        {
            throw new NotImplementedException();
        }
        public MedicoXClinicas Save(MedicoXClinicas entity)
        {
            try
            {
                var paciente = default(MedicoXClinicas);
                //var mXC = new MedicoXClinicas();
                switch (entity.ID)
                {
                    case 0:
                        //var bM = GetByMedicoId(entity.MedicoId);
                        //if (bM.Count() > 0)
                        //{
                        //    for (int i = 0; i < bM.Count(); i++)
                        //    {
                        //        mXC.ID = bM.ElementAtOrDefault(i).ID;
                        //        Delete(mXC);
                        //    }
                        //}
                        paciente = _repository.Add(entity).SingleOrDefault();
                        break;
                    default:
                        //var buscaMedico = GetByMedicoId(entity.MedicoId);
                        //if (buscaMedico.Count() > 0)
                        //{
                        //    for (int i = 0; i < buscaMedico.Count(); i++)
                        //    {
                        //        mXC.ID = buscaMedico.ElementAtOrDefault(i).ID;
                        //        Delete(mXC);
                        //    }
                        //}
                        paciente = Update(entity);
                        break;
                }
                return paciente;
            }
            catch (MedicoXClinicasException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new MedicoXClinicasException("Não foi possível salvar o vinculo informado.", e);
            }
        }
        public MedicoXClinicas Update(MedicoXClinicas entity)
        {
            try
            {
                _repository.Update(entity);
                return entity;
            }
            catch (Exception e)
            {
                throw new MedicoXClinicasException("Não foi possível atualizar o vinculo informado.", e);
            }
        }
        public IEnumerable<MedicoXClinicas> GetByMedicosIds(IEnumerable<int> medicosIds)
        {
            try
            {
                var result = _repository.GetList(mXe => medicosIds.Contains(mXe.MedicoId));
                return result;
            }
            catch (Exception e)
            {
                throw new MedicosXEspecialidadesException("Não foi possível recuperar os registros solicitados.", e);
            }
        }
        public IEnumerable<MedicoXClinicas> GetByMedicoId(int medicoId)
        {
            try
            {
                var result = _repository.GetList(mXe => mXe.MedicoId.Equals(medicoId));
                return result;
            }
            catch (Exception e)
            {
                throw new MedicosXEspecialidadesException("Não foi possível recuperar os registros solicitados.", e);
            }
        }
    }
}

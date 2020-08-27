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
    public class MedicosXEspecialidadesDomain : IDomain<MedicoXEspecialidade>
    {
        private readonly MedicosXEspecialidadesRepository _repository;

        public MedicosXEspecialidadesDomain(MedicosXEspecialidadesRepository repository)
        {
            _repository = repository;
        }

        public void Delete(MedicoXEspecialidade entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MedicoXEspecialidade> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MedicoXEspecialidade> GetAll(int idCliente)
        {
            throw new NotImplementedException();
        }

        public MedicoXEspecialidade GetById(int entityId, int idCliente)
        {
            throw new NotImplementedException();
        }

        public MedicoXEspecialidade Save(MedicoXEspecialidade entity)
        {
            try
            {
                var mXe = default(MedicoXEspecialidade);
                switch (entity.ID)
                {
                    case 0:
                        var r = _repository.Add(entity);
                        mXe = r.SingleOrDefault();
                        break;
                    default:
                        mXe = Update(entity);
                        break;
                }

                return mXe;
            }
            catch(MedicosXEspecialidadesException e)
            {
                throw e;
            }
            catch(Exception e)
            {
                throw new MedicosXEspecialidadesException("Não foi possível atualizar o registro informado.", e);
            }
        }

        public MedicoXEspecialidade Update(MedicoXEspecialidade entity)
        {
            try
            {
                _repository.Update(entity);
                return entity;
            }
            catch(Exception e)
            {
                throw new MedicosXEspecialidadesException("Não foi possível atualizar o registro informado.", e);
            }
        }

        public IEnumerable<MedicoXEspecialidade> GetByMedicosIds(IEnumerable<int> medicosIds)
        {
            try
            {
                var result = _repository.GetList(mXe => medicosIds.Contains(mXe.MedicoId));
                return result;
            }
            catch(Exception e)
            {
                throw new MedicosXEspecialidadesException("Não foi possível recuperar os registros solicitados.", e);
            }
        }

        public IEnumerable<MedicoXEspecialidade> GetByMedicoId(int medicoId)
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

        public IEnumerable<MedicoXEspecialidade> GetByIds(IEnumerable<int> ids, int idCliente)
        {
            try
            {
                var result = _repository.GetList(p => ids.Contains(p.MedicoId));
                return result;
            }
            catch (Exception e)
            {
                throw new MedicosXEspecialidadesException("Não foi possível buscar os medicos solicitados.", e);
            }
        }
    }
}

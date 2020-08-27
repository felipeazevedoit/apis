using System;
using System.Collections.Generic;
using System.Text;
using WpPacientes.Domains.Generics;
using WpPacientes.Entities;
using WpPacientes.Infrastructure;
using WpPacientes.Infrastructure.Exceptions;

namespace WpPacientes.Domains
{
    public class ConvenioDomain : IDomain<Convenio>
    {
        private readonly ConvenioRepository _repository;

        public ConvenioDomain(ConvenioRepository repository)
        {
            _repository = repository;
        }

        public void Delete(Convenio entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Convenio> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Convenio> GetAll(int idCliente)
        {
            try
            {
                var result = _repository.GetList(p => p.IdCliente.Equals(idCliente));
                return result;
            }
            catch (Exception e)
            {
                throw new ConvenioException("Não foi possível recuperar os convênios solicitados.", e);
            }
        }

        public Convenio GetById(int entityId, int idCliente)
        {
            throw new NotImplementedException();
        }

        public Convenio Save(Convenio entity)
        {
            throw new NotImplementedException();
        }

        public Convenio Update(Convenio entity)
        {
            throw new NotImplementedException();
        }
    }
}

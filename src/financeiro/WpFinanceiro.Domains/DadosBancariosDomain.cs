using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpFinanceiro.Domains.Generics;
using WpFinanceiro.Entities;
using WpFinanceiro.Infrastructure;
using WpFinanceiro.Infrastructure.Exceptions;

namespace WpFinanceiro.Domains
{
    public class DadosBancariosDomain : IDomain<DadosBancarios>
    {
        private readonly DadosBancariosRepository _repository;

        public DadosBancariosDomain(DadosBancariosRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<DadosBancarios> GetAll()
        {
            try
            {
                var result = _repository.GetAll();
                return result;
            }
            catch(Exception e)
            {
                throw new DadosBancariosException("Não foi possível recuperar as informações do usuário", e);
            }
        }

        public DadosBancarios Save(DadosBancarios entity)
        {
            try
            {
                switch (entity.ID)
                {
                    case 0:
                        entity.DataCriacao = DateTime.UtcNow;
                        entity.DataEdicao = DateTime.UtcNow;
                        entity.Ativo = true;
                        entity = _repository.Add(entity).SingleOrDefault();
                        break;
                    default:
                        Update(entity);
                        break;
                }

                return entity;
            }
            catch(Exception e)
            {
                throw new DadosBancariosException("Não foi possível salvar as informações do usuário", e);
            }
        }

        public void Update(DadosBancarios entity)
        {
            try
            {
                entity.DataEdicao = DateTime.UtcNow;
                _repository.Update(entity);
            }
            catch(Exception e)
            {
                throw new DadosBancariosException("Não foi possível atualizar as informações do usuário", e);
            }
        }

        public DadosBancarios GetByCpf(string cpf, int idCliente)
        {
            try
            {
                var result = _repository.GetSingle(d => d.IdCliente.Equals(idCliente) && d.Cpf.Equals(cpf));
                return result;
            }
            catch(Exception e)
            {
                throw new DadosBancariosException("Não foi possível recuperar as informações do usuário.", e);
            }
        }

        public DadosBancarios GetByCodigoExterno(int idCliente, int codigoExterno)
        {
            try
            {
                var result = _repository.GetSingle(d => d.CodigoExterno.Equals(codigoExterno) && d.IdCliente.Equals(idCliente));
                return result;
            }
            catch (Exception e)
            {
                throw new DadosBancariosException("Não foi possível recuperar as informações do usuário.", e);
            }
        }
    }
}

using Formulario.Dinamico.Domains.Generics;
using Formulario.Dinamico.Entities;
using Formulario.Dinamico.Infrastructure;
using Formulario.Dinamico.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Formulario.Dinamico.Domains
{
    public class CriptografiaDomain : IDomain<Criptografia>
    {
        private readonly CriptografiasRepository _repository;

        public CriptografiaDomain(CriptografiasRepository repository)
        {
            _repository = repository;
        }

        public Criptografia Delete(Criptografia entity)
        {
            try
            {
                entity.DateAlteracao = DateTime.Now;
                entity.Status = 9;
                entity.Ativo = false;

                _repository.Update(entity);

                return entity;
            }
            catch (Exception e)
            {
                throw new CriptografiaException("Não foi possível desativar a criptografia informada.", e);
            }
        }

        public IEnumerable<Criptografia> GetAll(int idCliente)
        {
            try
            {
                var result = _repository.GetList(c => c.IdCliente.Equals(idCliente) && c.Ativo);
                return result;
            }
            catch (Exception e)
            {
                throw new CriptografiaException("Não foi possível recuperar as criptografias solicitadas.", e);
            }
        }

        public Criptografia GetById(int entityId, int idCliente)
        {
            try
            {
                var result = _repository.GetSingle(c => c.ID.Equals(entityId) && c.IdCliente.Equals(idCliente));
                return result;
            }
            catch (Exception e)
            {
                throw new CriptografiaException("Não foi possível recuperar a criptografia solicitada.", e);
            }
        }

        public Criptografia GetByIdExterno(int idExterno, int idCliente)
        {
            try
            {
                var result = _repository.GetSingle(c => c.CodigoExterno.Equals(idExterno) && c.IdCliente.Equals(idCliente));
                return result;
            }
            catch (Exception e)
            {
                throw new CriptografiaException("Não foi possível recuperar a criptografia solicitada.", e);
            }
        }

        public Criptografia Save(Criptografia entity)
        {
            try
            {
                switch (entity.ID)
                {
                    case 0:
                        entity.DataCriacao = DateTime.Now;
                        entity.DateAlteracao = DateTime.Now;
                        entity.Ativo = true;
                        entity = _repository.Add(entity).SingleOrDefault();
                        break;
                    default:
                        Update(entity);
                        break;
                }

                return entity;
            }
            catch (CriptografiaException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new CriptografiaException("Não foi possível salvar as informações da criptografia.", e);
            }
        }

        public Criptografia Update(Criptografia entity)
        {
            try
            {
                entity.DateAlteracao = DateTime.Now;
                _repository.Update(entity);

                return entity;
            }
            catch (Exception e)
            {
                throw new CriptografiaException("Não foi possível atualizar a criptografia informada.", e);
            }
        }
    }
}

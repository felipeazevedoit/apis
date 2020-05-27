using Paginas.Api.Domains.Generics;
using Paginas.Api.Entities;
using Paginas.Api.Infrastructure;
using Paginas.Api.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paginas.Api.Domains
{
    public class PaginasDomain : IDomain<Pagina>
    {
        private readonly PaginasRepository _repository;

        public PaginasDomain(PaginasRepository repository)
        {
            _repository = repository;
        }

        public Pagina Delete(Pagina entity)
        {
            try
            {
                entity.DateAlteracao = DateTime.Now;
                entity.Status = 9;
                entity.Ativo = false;

                _repository.Update(entity);

                return entity;
            }
            catch(Exception e)
            {
                throw new PaginasException("Não foi possível desativar a página informada.", e);
            }
        }

        public IEnumerable<Pagina> GetAll(int idCliente)
        {
            try
            {
                var result = _repository.GetList(p => p.IdCliente.Equals(idCliente) && p.Ativo);
                return result;
            }
            catch(Exception e)
            {
                throw new PaginasException("Não foi possível recuperar as páginas solicitadas.", e);
            }
        }

        public IEnumerable<int> GetAllCodigos(int idCliente)
        {
            try
            {
                var result = _repository.GetList(p => p.IdCliente.Equals(idCliente) && p.Ativo);
                return result.Select(p => p.CodigoExterno);
            }
            catch (Exception e)
            {
                throw new PaginasException("Não foi possível recuperar as páginas solicitadas.", e);
            }
        }

        public Pagina GetById(int entityId, int idCliente)
        {
            try
            {
                var result = _repository.GetSingle(p => p.ID.Equals(entityId) && p.IdCliente.Equals(idCliente));
                return result;
            }
            catch (Exception e)
            {
                throw new PaginasException("Não foi possível recuperar a página solicitada.", e);
            }
        }

        public Pagina GetByCodigoExterno(int codigoExterno, int idCliente)
        {
            try
            {
                var result = _repository.GetSingle(p => p.CodigoExterno.Equals(codigoExterno) && p.IdCliente.Equals(idCliente));
                return result;
            }
            catch (Exception e)
            {
                throw new PaginasException("Não foi possível recuperar a página solicitada.", e);
            }
        }

        public Pagina Save(Pagina entity)
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
            catch(PaginasException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new PaginasException("Não foi possível salvar as informações da página.", e);
            }
        }

        public Pagina Update(Pagina entity)
        {
            try
            {
                entity.DateAlteracao = DateTime.Now;
                _repository.Update(entity);

                return entity;
            }
            catch(Exception e)
            {
                throw new PaginasException("Não foi possível atualizar a página informada.", e);
            }
        }
    }
}

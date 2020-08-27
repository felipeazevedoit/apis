using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpNoticias.Domains.Generics;
using WpNoticias.Entities;
using WpNoticias.Infrastructure;
using WpNoticias.Infrastructure.Exceptions;

namespace WpNoticias.Domains
{
    public class CategoriasDomain : IDomain<Categoria>
    {
        private readonly CategoriasRepository _repository;

        public CategoriasDomain(CategoriasRepository repository)
        {
            _repository = repository;
        }

        public void Delete(Categoria entity)
        {
            try
            {
                entity.Status = 9;
                entity.Ativo = false;

                _repository.Update(entity);
            }
            catch(Exception e)
            {
                throw new CategoriaException("Não foi possível desativar a categoria informada.");
            }
        }

        public IEnumerable<Categoria> GetAll()
        {
            try
            {
                var result = _repository.GetAll();
                return result;
            }
            catch (Exception e)
            {
                throw new CategoriaException("Não foi possível recuperar as categorias solicitadas");
            }
        }

        public IEnumerable<Categoria> GetAll(int idCliente)
        {
            try
            {
                var result = _repository.GetList(c => c.IdCliente.Equals(idCliente));
                return result;
            }
            catch (Exception e)
            {
                throw new CategoriaException("Não foi possível recuperar as categorias solicitadas");
            }
        }

        public Categoria GetById(int entityId, int idCliente)
        {
            try
            {
                var result = _repository.GetSingle(c => c.IdCliente.Equals(idCliente) && c.ID.Equals(entityId));
                return result;
            }
            catch (Exception e)
            {
                throw new CategoriaException("Não foi possível recuperar a categoria solicitada");
            }
        }

        public Categoria Save(Categoria entity)
        {
            try
            {
                var categoria = default(Categoria);

                switch (entity.ID)
                {
                    case 0:
                        entity.DataCriacao = DateTime.UtcNow;
                        entity.DataEdicao = DateTime.UtcNow;
                        //entity.Ativo = false;
                        //entity.Status = 9;
                        categoria = _repository.Add(entity).SingleOrDefault();
                        break;
                    default:
                        categoria = Update(entity);
                        break;
                }

                return categoria;
            }
            catch (Exception e)
            {
                throw new CategoriaException("Não foi possível completar a operação.", e);
            }
        }

        public Categoria Update(Categoria entity)
        {
            try
            {
                entity.DataEdicao = DateTime.UtcNow;

                _repository.Update(entity);

                return entity;
            }
            catch (Exception e)
            {
                throw new CategoriaException("Não foi possível atualizar a categoria informada.");
            }
        }
    }
}

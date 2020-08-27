using System;
using System.Collections.Generic;
using System.Linq;
using WpNoticias.Domains.Generics;
using WpNoticias.Entities;
using WpNoticias.Infrastructure;
using WpNoticias.Infrastructure.Exceptions;

namespace WpNoticias.Domains
{
    public class NoticiasDomain : IDomain<Noticia>
    {
        private readonly NoticiaRepository _repository;

        public NoticiasDomain(NoticiaRepository repository)
        {
            _repository = repository;
        }

        public void Delete(Noticia entity)
        {
            try
            {
                entity.DataEdicao = DateTime.UtcNow;
                entity.Ativo = false;
                entity.Status = 9;

                _repository.Update(entity);
            }
            catch(Exception e)
            {
                throw new NoticiaException("Não foi possível desativar a noticia informada.", e);
            }
        }

        public IEnumerable<Noticia> GetAll()
        {
            try
            {
                var result = _repository.GetAll().OrderByDescending(n => n.DataCriacao).ToList();
                return result;
            }
            catch(Exception e)
            {
                throw new NoticiaException("Não foi possível listar as noticias.", e);
            }
        }

        public IEnumerable<Noticia> GetAll(int idCliente)
        {
            try
            {
                var result = _repository.GetList(n => n.IdCliente.Equals(idCliente)).OrderByDescending(n => n.DataCriacao).ToList();
                return result;
            }
            catch(Exception e)
            {
                throw new NoticiaException("Não foi possível listar as noticias solicitadas.", e);
            }
        }

        public Noticia GetById(int entityId, int idCliente)
        {
            try
            {
                var result = _repository.GetSingle(n => n.ID.Equals(entityId) && n.IdCliente.Equals(idCliente));
                return result;
            }
            catch(Exception e)
            {
                throw new NoticiaException("Não foi possível recuperar a noticia solicitada.", e);
            }
        }

        public Noticia Save(Noticia entity)
        {
            try
            {
                var noticia = default(Noticia);

                switch (entity.ID)
                {
                    case 0:
                        entity.DataCriacao = DateTime.UtcNow;
                        entity.DataEdicao = DateTime.UtcNow;
                        //entity.Ativo = false;
                        //entity.Status = 9;
                        noticia = _repository.Add(entity).SingleOrDefault();
                        break;
                    default:
                        noticia = Update(entity);
                        break;
                }

                return noticia;
            }
            catch(Exception e)
            {
                throw new NoticiaException("Não foi possível completar a operação.", e);
            }
        }

        public Noticia Update(Noticia entity)
        {
            try
            {
                entity.DataEdicao = DateTime.UtcNow;
                _repository.Update(entity);

                return entity;
            }
            catch(Exception e)
            {
                throw new NoticiaException("Não foi possível atualizar a noticia informada.", e);
            }
        }

        public Noticia Publicar(Noticia entity)
        {
            try
            {
                entity.DataEdicao = DateTime.UtcNow;
                entity.Ativo = true;
                entity.Status = 1;
                _repository.Update(entity);

                return entity;
            }
            catch (Exception e)
            {
                throw new NoticiaException("Não foi possível publicar a noticia informada.", e);
            }
        }

        public IEnumerable<Noticia> GetByTags(int idCliente, int codigoExterno, IEnumerable<string> tags)
        {
            try
            {
                var result = _repository.GetList(n => n.Tags.Split(',').Any(t => tags.Contains(t)) 
                    && n.CodigoExterno.Equals(codigoExterno) && n.IdCliente.Equals(idCliente)).OrderByDescending(n => n.DataCriacao).ToList();

                if (result == null || result.Count == 0)
                {
                    result = _repository.GetList(n => n.CodigoExterno.Equals(codigoExterno) 
                        && n.IdCliente.Equals(idCliente)).OrderByDescending(n => n.DataCriacao).ToList();
                }

                return result;
            }
            catch(Exception e)
            {
                throw new NoticiaException("Não foi possível buscar as noticias solicitadas.", e);
            }
        }

        public IEnumerable<Noticia> GetNoticiasPublicas(int idCliente)
        {
            try
            {
                var result = _repository.GetList(n => !n.Privado && n.Ativo 
                    && n.IdCliente.Equals(idCliente)).OrderByDescending(n => n.DataCriacao).ToList();

                return result;
            }
            catch(Exception e)
            {
                throw new NoticiaException("Não foi possível buscar as noticias solicitadas.", e);
            }
        }

        public IEnumerable<Noticia> GetNoticiasPrivadas(int idCliente)
        {
            try
            {
                var result = _repository.GetList(n => n.Ativo
                    && n.IdCliente.Equals(idCliente)).OrderByDescending(n => n.DataCriacao).ToList();

                return result;
            }
            catch (Exception e)
            {
                throw new NoticiaException("Não foi possível buscar as noticias solicitadas.", e);
            }
        }

        public IEnumerable<Noticia> GetByCodigoExterno(int idCliente, int codigoExterno)
        {
            try
            {
                var result = _repository.GetList(n => n.IdCliente.Equals(idCliente) 
                    && n.CodigoExterno.Equals(codigoExterno)).OrderByDescending(n => n.DataCriacao).ToList();

                return result;
            }
            catch (Exception e)
            {
                throw new NoticiaException("Não foi possível buscar as noticias solicitadas.", e);
            }
        }

        public IEnumerable<Noticia> GetByCategoria(int idCliente, int categoriaId)
        {
            try
            {
                var result = _repository.GetList(n => n.IdCliente.Equals(idCliente) && n.CategoriaId.Equals(categoriaId)).OrderByDescending(n => n.DataCriacao).ToList();

                return result;
            }
            catch(Exception e)
            {
                throw new NoticiaException("Não foi possível buscar as noticias solicitadas.", e);
            }
        }

        public IEnumerable<Noticia> Pesquisar(int idCliente, string texto)
        {
            try
            {
                var result = _repository.GetList(p => !string.IsNullOrEmpty(p.Nome) && !string.IsNullOrEmpty(p.Descricao)?(p.Nome.ToUpper().Contains(texto.ToUpper()) || p.Descricao.ToUpper().Contains(texto.ToUpper())) && p.IdCliente.Equals(idCliente) : false);
                return result;
            }
            catch (Exception e)
            {
                throw new NoticiaException("Não foi possível buscar as noticias solicitadas.", e);
            }
        }
    }
}

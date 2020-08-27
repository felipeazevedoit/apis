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
    public class ComentariosDomain : IDomain<Comentario>
    {
        private readonly ComentarioRepository _repository;

        public ComentariosDomain(ComentarioRepository repository)
        {
            _repository = repository;
        }

        public void Delete(Comentario entity)
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
                throw new NoticiaException("Não foi possível desativar o comentário informado.", e);
            }
        }

        public IEnumerable<Comentario> GetAll()
        {
            try
            {
                var result = _repository.GetAll();
                return result;
            }
            catch (Exception e)
            {
                throw new ComentarioException("Não foi possível listar os comentários.", e);
            }
        }

        public IEnumerable<Comentario> GetAll(int idCliente)
        {
            try
            {
                var result = _repository.GetList(n => n.IdCliente.Equals(idCliente));
                return result;
            }
            catch (Exception e)
            {
                throw new ComentarioException("Não foi possível listar os comentários solicitadas.", e);
            }
        }

        public Comentario GetById(int entityId, int idCliente)
        {
            try
            {
                var result = _repository.GetSingle(n => n.ID.Equals(entityId) && n.IdCliente.Equals(idCliente));
                return result;
            }
            catch (Exception e)
            {
                throw new ComentarioException("Não foi possível recuperar o comentário solicitada.", e);
            }
        }

        public Comentario Save(Comentario entity)
        {
            try
            {
                var noticia = default(Comentario);

                switch (entity.ID)
                {
                    case 0:
                        entity.DataCriacao = DateTime.UtcNow;
                        entity.DataEdicao = DateTime.UtcNow;
                        entity.Ativo = false;
                        entity.Status = 9;
                        noticia = _repository.Add(entity).SingleOrDefault();
                        break;
                    default:
                        noticia = Update(entity);
                        break;
                }

                return noticia;
            }
            catch (Exception e)
            {
                throw new ComentarioException("Não foi possível completar a operação.", e);
            }
        }

        public Comentario Update(Comentario entity)
        {
            try
            {
                entity.DataEdicao = DateTime.UtcNow;
                _repository.Update(entity);

                return entity;
            }
            catch (Exception e)
            {
                throw new ComentarioException("Não foi possível atualizar o comentário informado.", e);
            }
        }

        public IEnumerable<Comentario> GetByNoticiaId(int noticiaId, int idCliente)
        {
            try
            {
                var result = _repository.GetList(c => c.NoticiaId.Equals(noticiaId) && c.IdCliente.Equals(idCliente));
                return result;
            }
            catch (Exception e)
            {
                throw new ComentarioException("Não foi possível recuperar os comentários solicitados.", e);
            }
        }
    }
}

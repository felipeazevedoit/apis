using Mensagens.Domains.Generics;
using Mensagens.Entities;
using Mensagens.Infrastructure;
using Mensagens.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mensagens.Domains
{
    public class MensagensDomain : IDomain<Mensagem>
    {
        private readonly MensagensRepository _repository;

        public MensagensDomain(MensagensRepository repository)
        {
            _repository = repository;
        }

        public Mensagem Delete(Mensagem entity)
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
                throw new MensagensException("Não foi possível desativar a mensagem informada.", e);
            }
        }

        public IEnumerable<Mensagem> GetAll(int idCliente)
        {
            var result = _repository.GetList(p => p.IdCliente.Equals(idCliente));
            return result;
        }

        public Mensagem GetById(int entityId, int idCliente)
        {
            try
            {
                var result = _repository.GetSingle(p => p.ID.Equals(entityId) && p.IdCliente.Equals(idCliente));
                return result;
            }
            catch (Exception e)
            {
                throw new MensagensException("Não foi possível recuperar a mensagem solicitada.", e);
            }
        }

        public IEnumerable<Mensagem> GetByIdRemetente(int idRemetente, int idCliente)
        {
            try
            {
                var result = _repository.GetList(p => p.RemetenteId.Equals(idRemetente) && p.IdCliente.Equals(idCliente));
                return result;
            }
            catch (Exception e)
            {
                throw new MensagensException("Não foi possível recuperar as mensagens solicitadas.", e);
            }
        }

        public IEnumerable<Mensagem> GetByIdDestinatario(int idDestinatario, int idCliente)
        {
            try
            {
                var result = _repository.GetList(p => p.DestinatarioId.Equals(idDestinatario) && p.IdCliente.Equals(idCliente));
                return result;
            }
            catch (Exception e)
            {
                throw new MensagensException("Não foi possível recuperar as mensagens solicitadas.", e);
            }
        }

        public Mensagem Save(Mensagem entity)
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
            catch (MensagensException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new MensagensException("Não foi possível salvar as informações da mensagem.", e);
            }
        }

        public Mensagem Update(Mensagem entity)
        {
            try
            {
                entity.DateAlteracao = DateTime.Now;
                _repository.Update(entity);

                return entity;
            }
            catch (Exception e)
            {
                throw new MensagensException("Não foi possível atualizar a mensagem informada.", e);
            }
        }

        public IEnumerable<Mensagem> GetByIdEntidade(int destinatarioId, int remetenteId, int idCliente)
        {
            try
            {
                var result = _repository.GetList(x => x.DestinatarioId.Equals(destinatarioId)
                    && x.RemetenteId.Equals(remetenteId) && x.IdCliente.Equals(idCliente));

                return result;
            }
            catch(MensagensException e)
            {
                throw e;
            }
            catch(Exception e)
            {
                throw new MensagensException("Não foi possível recuperar as mensagens da entidade informada.", e);
            }
        }
    }
}

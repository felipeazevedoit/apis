using Formulario.Dinamico.Domains.Generics;
using Formulario.Dinamico.Entities;
using Formulario.Dinamico.Infrastructure;
using Formulario.Dinamico.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Formulario.Dinamico.Domains
{
    public class PerguntasDomain : IDomain<Pergunta>
    {
        private readonly PerguntasRepository _repository;

        public PerguntasDomain(PerguntasRepository repository)
        {
            _repository = repository;
        }

        public Pergunta Delete(Pergunta entity)
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
                throw new PerguntaException("Não foi possível desativar a pergunta informada.", e);
            }
        }

        public IEnumerable<Pergunta> GetAll(int idCliente)
        {
            var result = _repository.GetList(p => p.IdCliente.Equals(idCliente));
            return result;
        }

        public async Task<IEnumerable<Pergunta>> GetAllAsync(int idCliente, int idExterno)
        {
            try
            {
                var result = await _repository.GetPerguntasAsync(idCliente, idExterno);
                return result;
            }
            catch (Exception e)
            {
                throw new PerguntaException("Não foi possível recuperar as perguntas solicitadas.", e);
            }
        }

        public Pergunta GetById(int entityId, int idCliente)
        {
            try
            {
                var result = _repository.GetSingle(p => p.ID.Equals(entityId) && p.IdCliente.Equals(idCliente));
                return result;
            }
            catch (Exception e)
            {
                throw new PerguntaException("Não foi possível recuperar a pergunta solicitada.", e);
            }
        }

        public Pergunta Save(Pergunta entity)
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
            catch (PerguntaException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new PerguntaException("Não foi possível salvar as informações da pergunta.", e);
            }
        }

        public Pergunta Update(Pergunta entity)
        {
            try
            {
                entity.DateAlteracao = DateTime.Now;
                _repository.Update(entity);

                return entity;
            }
            catch (Exception e)
            {
                throw new PerguntaException("Não foi possível atualizar a pergunta informada.", e);
            }
        }
    }
}

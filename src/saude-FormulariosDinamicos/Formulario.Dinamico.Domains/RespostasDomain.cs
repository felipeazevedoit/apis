using Formulario.Dinamico.Domains.Generics;
using Formulario.Dinamico.Entities;
using Formulario.Dinamico.Infrastructure;
using Formulario.Dinamico.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Formulario.Dinamico.Domains
{
    public class RespostasDomain : IDomain<Resposta>
    {
        private readonly RespostasRepository _repository;

        public RespostasDomain(RespostasRepository repository)
        {
            _repository = repository;
        }

        public Resposta Delete(Resposta entity)
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
                throw new RespostaException("Não foi possível desativar a resposta informada.", e);
            }
        }

        public IEnumerable<Resposta> GetAll(int idCliente)
        {
            try
            {
                var result = _repository.GetList(p => p.IdCliente.Equals(idCliente) && p.Ativo);
                return result;
            }
            catch (Exception e)
            {
                throw new RespostaException("Não foi possível recuperar as respostas solicitadas.", e);
            }
        }

        public Resposta GetById(int entityId, int idCliente)
        {
            try
            {
                var result = _repository.GetSingle(p => p.ID.Equals(entityId) && p.IdCliente.Equals(idCliente));
                return result;
            }
            catch (Exception e)
            {
                throw new RespostaException("Não foi possível recuperar a resposta solicitada.", e);
            }
        }

        public IEnumerable<Resposta> GetByPerguntaId(int perguntaId, int idCliente)
        {
            try
            {
                var respostas = _repository.GetList(r => r.PerguntaId.Equals(perguntaId) && r.IdCliente.Equals(idCliente));
                return respostas;
            }
            catch(Exception e)
            {
                throw new RespostaException("Não foi possível buscar as respostas solicitadas.", e);
            }
        }

        public IEnumerable<Resposta> GetByPerguntasIds(IEnumerable<int> perguntasIds, int idCliente)
        {
            try
            {
                var respostas = _repository.GetList(r => perguntasIds.Contains(r.PerguntaId) && r.IdCliente.Equals(idCliente));
                return respostas;
            }
            catch (Exception e)
            {
                throw new RespostaException("Não foi possível buscar as respostas solicitadas.", e);
            }
        }

        public Resposta Save(Resposta entity)
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
            catch (RespostaException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new RespostaException("Não foi possível salvar as informações da respostas.", e);
            }
        }

        public Resposta Update(Resposta entity)
        {
            try
            {
                entity.DateAlteracao = DateTime.Now;
                _repository.Update(entity);

                return entity;
            }
            catch (Exception e)
            {
                throw new RespostaException("Não foi possível atualizar a resposta informada.", e);
            }
        }

        public IEnumerable<Resposta> GetByIdExterno(int idExterno, int idCliente)
        {
            try
            {
                var respostas = _repository.GetList(r => 
                    r.CodigoExterno.Equals(idExterno) && r.IdCliente.Equals(idCliente));
                return respostas;
            }
            catch (Exception e)
            {
                throw new RespostaException("Não foi possível buscar as respostas solicitadas.", e);
            }
        }
    }
}

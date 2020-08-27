using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpPacientes.Domains.Generics;
using WpPacientes.Entities;
using WpPacientes.Infrastructure;
using WpPacientes.Infrastructure.Exceptions;

namespace WpPacientes.Domains
{
    public class GrupoDomain : IDomain<Grupo>
    {
        private readonly GrupoRepository _repository;

        public GrupoDomain(GrupoRepository repository)
        {
            _repository = repository;
        }

        public void Delete(Grupo entity)
        {
            try
            {
                entity.DataEdicao = DateTime.UtcNow;
                entity.Status = 9;
                entity.Ativo = false;

                _repository.Update(entity);
            }
            catch (Exception e)
            {
                throw new GrupoException("Não foi possível desativar o grupo informado.", e);
            }
        }

        public IEnumerable<Grupo> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Grupo> GetAll(int idCliente)
        {
            try
            {
                var result = _repository.GetList(x => x.IdCliente.Equals(idCliente));
                return result;
            }
            catch (Exception e)
            {
                throw new GrupoException("Não foi possível recuperar os grupos disponíveis.", e);
            }
        }

        public Grupo GetById(int entityId, int idCliente)
        {
            try
            {
                var result = _repository.GetSingle(g => g.ID.Equals(entityId) && g.IdCliente.Equals(idCliente));
                return result;
            }
            catch (Exception e)
            {
                throw new GrupoException("Não foi possível recuperar o grupo solicitado.", e);
            }
        }

        public Grupo Save(Grupo entity)
        {
            try
            {
                var paciente = default(Grupo);
                switch (entity.ID)
                {
                    case 0:
                        entity.DataCriacao = DateTime.UtcNow;
                        entity.DataEdicao = DateTime.UtcNow;
                        //entity.Status = 9;
                        //entity.Ativo = false;

                        paciente = _repository.Add(entity).SingleOrDefault();
                        break;
                    default:
                        paciente = Update(entity);
                        break;
                }

                return paciente;
            }
            catch (GrupoException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new GrupoException("Não foi possível salvar o grupo informado.", e);
            }
        }

        public Grupo Update(Grupo entity)
        {
            try
            {
                entity.DataEdicao = DateTime.UtcNow;
                _repository.Update(entity);

                return entity;
            }
            catch (Exception e)
            {
                throw new GrupoException("Não foi possível atualizar o grupo informado.", e);
            }
        }
    }
}
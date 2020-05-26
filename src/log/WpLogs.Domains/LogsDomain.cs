using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpLogs.Domains.Generics;
using WpLogs.Entities;
using WpLogs.Infrastructure;
using WpLogs.Infrastructure.Exceptions;

namespace WpLogs.Domains
{
    public class LogsDomain : IDomain<Log>
    {
        private readonly LogsRepository _repository;

        public LogsDomain(LogsRepository repository)
        {
            _repository = repository;
        }

        public void Delete(Log entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Log> GetAll(int idCliente)
        {
            try
            {
                var result = _repository.GetList(l => l.IdCliente.Equals(idCliente));
                return result;
            }
            catch(Exception e)
            {
                throw new LogException("Não foi possível recuperar os logs solicitados.", e);
            }
        }

        public IEnumerable<Log> GetAll()
        {
            try
            {
                var result = _repository.GetAll();
                return result;
            }
            catch (Exception e)
            {
                throw new LogException("Não foi possível recuperar os logs solicitados.", e);
            }
        }

        public Log GetById(int entityId)
        {
            throw new NotImplementedException();
        }

        public Log Save(Log entity)
        {
            try
            {
                entity.DataCriacao = DateTime.UtcNow;
                entity.Ativo = true;
                entity.Status = 1;

                var log = _repository.Add(entity).SingleOrDefault();
                return log;
            }
            catch(Exception e)
            {
                throw new LogException("Não foi possível salvar o registro de log informado.", e);
            }
        }

        public Log Update(Log entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Log> GetByRangeOfDate(int idCliente, DateTime incio, DateTime? fim = default(DateTime?))
        {
            try
            {
                var logs = default(IEnumerable<Log>);
                if (fim != null)
                {
                    logs = _repository.GetList(l =>
                        l.DataCriacao.Date >= incio.Date && l.DataCriacao.Date <= fim.Value.Date && l.IdCliente.Equals(idCliente));
                    return logs;
                }

                logs = _repository.GetList(l => l.DataCriacao.Date >= incio.Date);
                return logs;
            }
            catch(Exception e)
            {
                throw new LogException("Não foi possível recuperar os logs solicitados.", e);
            }
        }

        public IEnumerable<Log> GetByUsuario(int usuarioCriacao, int idCliente)
        {
            try
            {
                var logs = _repository.GetList(l => 
                    l.UsuarioCriacao.Equals(usuarioCriacao) && l.IdCliente.Equals(idCliente));
                return logs;
            }
            catch(Exception e)
            {
                throw new LogException("Não foi possível recuperar os logs solicitados.", e);
            }
        }
    }
}

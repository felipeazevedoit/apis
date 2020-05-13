using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WpFinanceiro.Domains.Generics;
using WpFinanceiro.Entities;
using WpFinanceiro.Infrastructure;
using WpFinanceiro.Infrastructure.Exceptions;

namespace WpFinanceiro.Domains
{
    public class ExtratoDomain : IDomain<Extrato>
    {
        private readonly ExtratoRepository _repository;
        private readonly NaturezaRepository _nRepository;

        public ExtratoDomain(ExtratoRepository repository, NaturezaRepository nRepository)
        {
            _repository = repository;
            _nRepository = nRepository;
        }

        public IEnumerable<Extrato> Inserir(IEnumerable<Extrato> entities)
        {
            try
            {
                foreach (var item in entities)
                {
                    item.DataCriacao = DateTime.UtcNow;
                    item.DataEdicao = DateTime.UtcNow;
                    item.Ativo = true;
                    item.StatusId = Status.Aprovado; //Aprovado
                }

                var extratos = _repository.Add(entities.ToArray());

                return extratos;
            }
            catch(Exception e)
            {
                throw new ExtratoException("Não foi possível salvar os extratos recebidos na requisição.", e);
            }
        }

        public IEnumerable<Extrato> Alocar(IEnumerable<Extrato> entities)
        {
            try
            {
                foreach (var item in entities)
                {
                    item.DataCriacao = DateTime.UtcNow;
                    item.DataEdicao = DateTime.UtcNow;
                    item.Ativo = true;
                }

                var extratos = _repository.Add(entities.ToArray());

                return extratos;
            }
            catch (Exception e)
            {
                throw new ExtratoException("Não foi possível salvar os extratos recebidos na requisição.", e);
            }
        }

        public bool LiberarPagamento(int idCliente, int codigoExterno, string destino, int tipoDestino)
        {
            var extrato = _repository.GetSingle(e => e.IdCliente.Equals(idCliente)
                                && e.CodigoExterno.Equals(codigoExterno) && e.Destino.Equals(destino) && e.TipoDestino.Equals(tipoDestino));
            if (Status.Bloqueado.Equals(extrato.StatusId)) //Bloqueado
            {
                extrato.StatusId = Status.Aprovado; //Aprovado
                _repository.Update(extrato);
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Extrato>> GetByProperties(IDictionary<string, object> properties)
        {
            try
            {
                IEnumerable<Extrato> extratos = default(List<Extrato>);
                var query = string.Empty;
                if (properties.Count > 0)
                {
                    var props = string.Join(",", properties.Select(p => p.Key));
                    query = $"SELECT * FROM Extratos WHERE ";

                    for (int i = 0; i < properties.Count; i++)
                    {
                        if ((i + 1) < properties.Count)
                            query += $"{ properties.ElementAt(i).Key } = { properties.ElementAt(i).Value } and ";
                        else
                            query += $"{ properties.ElementAt(i).Key } = { properties.ElementAt(i).Value }";
                    }
                }
                else
                {
                    query = "SELECT * FROM EXTRATOS";
                }

                extratos = await _repository.GetByQueryAsync(query);
                return extratos;
            }
            catch (Exception e)
            {
                throw new ExtratoException("Não foi possível listar os extratos solicitados.", e);
            }
        }

        public IEnumerable<Extrato> GetByRangeOfDate(DateTime dataInicio, DateTime dataFim)
        {
            try
            {
                var result = _repository.GetList(e => e.DataCriacao >= dataInicio && e.DataCriacao <= dataFim);
                return result;
            }
            catch(Exception e)
            {
                throw new ExtratoException("Não foi possível listar os extratos solicitados.", e);
            }
        }

        public decimal GetSaldo(int idCliente, string destino, int tipoDestino)
        {
            try
            {
                var extratos = _repository.GetList(e => e.IdCliente.Equals(idCliente) 
                    && e.Destino.Equals(destino) && e.TipoDestino.Equals(tipoDestino));
                var saldo = extratos.Sum(e => e.Valor);

                return saldo;
            }
            catch (Exception e)
            {
                throw new ExtratoException("Não foi possível buscar o saldo solicitado.", e);
            }
        }

        public IEnumerable<Extrato> GetSaldoAprovado(int idCliente, string destino, int tipoDestino)
        {
            try
            {
                var extratos = _repository.GetList(e => e.IdCliente.Equals(idCliente)
                    && e.Destino.Equals(destino) && e.TipoDestino.Equals(tipoDestino) && e.StatusId == Status.Aprovado);

                return extratos;
            }
            catch (Exception e)
            {
                throw new ExtratoException("Não foi possível buscar o saldo solicitado.", e);
            }
        }

        public IEnumerable<Extrato> GetSaldoPendente(int idCliente, string destino, int tipoDestino)
        {
            try
            {
                var extratos = _repository.GetList(e => e.IdCliente.Equals(idCliente)
                    && e.Destino.Equals(destino) && e.TipoDestino.Equals(tipoDestino) && e.StatusId == Status.Aguardando);

                return extratos;
            }
            catch (Exception e)
            {
                throw new ExtratoException("Não foi possível buscar o saldo solicitado.", e);
            }
        }

        public IEnumerable<Extrato> GetSByDestino(int idCliente, string destino, int tipoDestino)
        {
            try
            {
                var extratos = _repository.GetList(e => e.IdCliente.Equals(idCliente)
                    && e.Destino.Equals(destino) && e.TipoDestino.Equals(tipoDestino));

                return extratos;
            }
            catch (Exception e)
            {
                throw new ExtratoException("Não foi possível buscar o saldo solicitado.", e);
            }
        }


        public IEnumerable<Natureza> GetNaturezas()
        {
            try
            {
                var naturezas = _nRepository.GetAll();
                return naturezas;
            }
            catch (Exception e)
            {
                throw new ExtratoException("Não foi possível buscar as naturezas.", e);
            }
        }

        public IEnumerable<Extrato> GetAll()
        {
            throw new NotImplementedException();
        }

        public Extrato Save(Extrato entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Extrato entity)
        {
            throw new NotImplementedException();
        }
    }
}

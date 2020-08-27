using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpMedicos.Domains.Generics;
using WpMedicos.Entities;
using WpMedicos.Infrastructure;
using WpMedicos.Infrastructure.Exceptions;
using WpNoticias.Services;

namespace WpMedicos.Domains
{
    public class TelefoneDomain : IDomain<Telefone>
    {
        private readonly TelefoneRepository _repository;
        private readonly SegurancaService _segService;

        public TelefoneDomain(TelefoneRepository repository, SegurancaService segService)
        {
            _repository = repository;
            _segService = segService;
        }
        public async Task DeleteAsync(int medicoId, string token)
        {
            try
            {
                await _segService.ValidateTokenAsync(token);
                var telefone = _repository.GetList(t => t.MedicoId.Equals(medicoId)).SingleOrDefault();

                if (telefone != null)
                {
                    telefone.Status = 9;
                    telefone.Ativo = false;
                    _repository.Update(telefone);
                }
            }
            catch (ServiceException e)
            {
                throw e;
            }
            catch (InvalidTokenException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new EnderecoException("Não foi possível remover o endereço da oportunidade.", e);
            }
        }
        public async Task<Telefone> SaveAsync(Telefone entity, string token)
        {
            try
            {
                if (entity != null && !string.IsNullOrEmpty(entity.Numero))
                {
                    await _segService.ValidateTokenAsync(token);

                    switch (entity.ID)
                    {
                        case 0:
                            entity.DataCriacao = DateTime.UtcNow;
                            entity.DataEdicao = DateTime.UtcNow;
                            entity.Ativo = true;
                            entity.ID = _repository.Add(entity).FirstOrDefault().ID;
                            break;
                        default:
                            entity = await UpdateAsync(entity, token);
                            break;
                    }

                }
                return entity;
            }
            catch (ServiceException e)
            {
                throw e;
            }
            catch (InvalidTokenException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new TelefoneException("Não foi possível salvar o telefone da empresa. Entre em contato com o suporte.", e);
            }
        }
        public async Task<Telefone> UpdateAsync(Telefone entity, string token)
        {
            try
            {
                await _segService.ValidateTokenAsync(token);
                entity.DataEdicao = DateTime.UtcNow;
                _repository.Update(entity);

                return entity;
            }
            catch (ServiceException e)
            {
                throw e;
            }
            catch (InvalidTokenException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new TelefoneException("Não foi possível atualizar o telefone da empresa. Entre em contato com o suporte.", e);
            }
        }
        public IEnumerable<Telefone> GetAlls(IEnumerable<int> medicoId, string token)
        {
            try
            {
                //await _segService.ValidateTokenAsync(token);
                var telefones = _repository.GetList(t => medicoId.Contains(t.MedicoId));

                return telefones;
            }
            catch (ServiceException e)
            {
                throw e;
            }
            catch (InvalidTokenException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new TelefoneException("Não foi possível recuperar o telefone.", e);
            }
        }

        public async Task<Telefone> GetByMedicoIdAsync(int medicoId, int idCliente, string token)
        {
            try
            {
                await _segService.ValidateTokenAsync(token);

                var telefone = _repository.GetList(t => t.MedicoId.Equals(medicoId)
                                        && t.IdCliente.Equals(idCliente)).SingleOrDefault();
                return telefone;
            }
            catch (ServiceException e)
            {
                throw e;
            }
            catch (InvalidTokenException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new TelefoneException("Não foi possível recuperar o telefone da empresa. Entre em contato com o suporte.", e);
            }
        }

        public async Task<Telefone> GetByCodExternoAsync(int codExterno, int idCliente, string token)
        {
            try
            {
                await _segService.ValidateTokenAsync(token);

                var telefone = _repository.GetList(t => t.UsuarioCriacao.Equals(codExterno)
                                        && t.IdCliente.Equals(idCliente)).SingleOrDefault();
                return telefone;
            }
            catch (ServiceException e)
            {
                throw e;
            }
            catch (InvalidTokenException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new TelefoneException("Não foi possível recuperar o telefone da empresa. Entre em contato com o suporte.", e);
            }
        }

        public void Delete(Telefone entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Telefone> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Telefone> GetAll(int idCliente)
        {
            throw new NotImplementedException();
        }

        public Telefone GetById(int entityId, int idCliente)
        {
            throw new NotImplementedException();
        }

        public Telefone Save(Telefone entity)
        {
            throw new NotImplementedException();
        }

        public Telefone Update(Telefone entity)
        {
            throw new NotImplementedException();
        }
    }
}

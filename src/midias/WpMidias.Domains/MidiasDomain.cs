using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WpMidias.Domains.Generics;
using WpMidias.Domains.Helpers;
using WpMidias.Domains.Helpers.Exceptions;
using WpMidias.Entities;
using WpMidias.Infrastructure;
using WpMidias.Infrastructure.Exceptions;

namespace WpMidias.Domains
{
    public class MidiasDomain : IDomain<Midia>
    {
        //private const string _basePath = "wwwroot/";
        private const string _basePath = @"C:\inetpub\wwwroot\talanservices.com.br\";

        private readonly IConfiguration _configuration;
        private readonly MidiasRepository _repository;
        private readonly FileSystemManager _fManager;

        public MidiasDomain(MidiasRepository repository, FileSystemManager fManager, IConfiguration configuration)
        {
            _repository = repository;
            _fManager = fManager;
            _configuration = configuration;
        }

        public void Delete(Midia entity)
        {
            try
            {
                _repository.Remove(entity);

                var fileName = $"{ entity.Nome }{ entity.Extensao }";
                _fManager.RemoveFile(_basePath, fileName);
            }
            catch(Exception e)
            {
                throw new MidiaException("Não foi possível desativar a mídia informada.", e);
            }
        }

        public IEnumerable<Midia> GetAll()
        {
            try
            {
                var result = _repository.GetAll();
                return result;
            }
            catch(Exception e)
            {
                throw new MidiaException("Não foi possível listar as mídias disponíveis.", e);
            }
        }

        public IEnumerable<Midia> GetAll(int idCliente)
        {
            try
            {
                var result = _repository.GetList(m => m.IdCliente.Equals(idCliente));
                return result;
            }
            catch (Exception e)
            {
                throw new MidiaException("Não foi possível listar as mídias solicitadas.", e);
            }
        }

        public async Task<IEnumerable<Midia>> GetAllAsync(int idCliente)
        {
            try
            {
                var result = await _repository.GetMidiasAsync(idCliente);
                return result;
            }
            catch(Exception e)
            {
                throw new MidiaException("Não foi possível listar as mídias solicitadas.", e);
            }
        }

        public Midia GetById(int entityId, int idCliente)
        {
            try
            {
                var result = _repository.GetSingle(m => m.ID.Equals(entityId) && m.IdCliente.Equals(idCliente));
                return result;
            }
            catch (Exception e)
            {
                throw new MidiaException("Não foi possível recuperar a mídia selecionada.", e);
            }
        }

        public async Task<Midia> GetByIdAsync(int entityId, int idCliente)
        {
            try
            {
                var result = await _repository.GetMidiaAsync(entityId, idCliente);

                if (!string.IsNullOrEmpty(result.CaminhoFisico))
                {
                    var fileName = $"{ result.Nome }{ result.Extensao }";

                    result.Arquivo = await _fManager.GetFileAsync(_basePath, fileName);
                }

                return result;
            }
            catch(FileSystemException e)
            {
                throw e;
            }
            catch(Exception e)
            {
                throw new MidiaException("Não foi possível recuperar a mídia selecionada.", e);
            }
        }

        public Midia Save(Midia entity)
        {
            try
            {
                var midia = default(Midia);
                switch (entity.ID)
                {
                    case 0:
                        if (string.IsNullOrEmpty(entity.CaminhoVirtual))
                        {
                            var fileName = $"{ entity.Nome }{ entity.Extensao }";

                            _fManager.SaveFile(_basePath, fileName, entity.Arquivo);
                            var virtualPath = $"{ _configuration.GetValue<string>("VirtualPathBase") }/{ fileName }";

                            entity.CaminhoFisico = $"{ _basePath }/{ fileName }";
                            entity.CaminhoVirtual = virtualPath;
                        }

                        entity.DataCriacao = DateTime.UtcNow;
                        entity.DataEdicao = DateTime.UtcNow;
                        //entity.Status = 9;
                        //entity.Ativo = false;

                        midia = _repository.Add(entity).SingleOrDefault();                        
                        break;
                    default:
                        midia = Update(entity);
                        break;
                }

                return midia;
            }
            catch(FileSystemException e)
            {
                throw e;
            }
            catch(MidiaException e)
            {
                throw e;
            }
            catch(Exception e)
            {
                throw new MidiaException("Não foi possível salvar a mídia informada.", e);
            }
        }

        public async Task<IEnumerable<Midia>> GetByCodigoExternoAsync(int codigoExterno, int idCliente)
        {
            try
            {
                var result = await _repository.GetMidiaByIdExternoAsync(codigoExterno, idCliente);

                foreach (var item in result)
                {
                    if (!string.IsNullOrEmpty(item.CaminhoFisico))
                    {
                        var fileName = $"{ item.Nome }{ item.Extensao }";

                        item.Arquivo = await _fManager.GetFileAsync(_basePath, fileName);
                    }
                }              

                return result;
            }
            catch (FileSystemException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new MidiaException("Não foi possível recuperar a mídia selecionada.", e);
            }
        }

        public async Task<IEnumerable<Midia>> GetByCodigosExternosAsync(IEnumerable<int> codigos, int idCliente)
        {
            try
            {
                var result = await _repository.GetMidiaByIdsExternosAsync(codigos, idCliente);

                foreach (var item in result)
                {
                    if (!string.IsNullOrEmpty(item.CaminhoFisico))
                    {
                        var fileName = $"{ item.Nome }{ item.Extensao }";

                        item.Arquivo = await _fManager.GetFileAsync(_basePath, fileName);
                    }
                }

                return result;
            }
            catch (FileSystemException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new MidiaException("Não foi possível recuperar a mídia selecionada.", e);
            }
        }

        public async Task<IEnumerable<Midia>> GetByCodExternoFromTake(int codigoExterno, int idCliente,  int lastid,  int take)
        {
            try
            {
                return await _repository.GetByCodExternoFromTake(codigoExterno, idCliente, lastid, take);
            }
            catch (Exception e)
            {
                throw new MidiaException("Não foi possível recuperar a mídia selecionada.", e);
            }
        }

        public Midia Update(Midia entity)
        {
            try
            {
                if (string.IsNullOrEmpty(entity.CaminhoVirtual))
                {
                    var fileName = $"{ entity.Nome }{ entity.Extensao }";

                    _fManager.UpdateFile(_basePath, fileName, entity.Arquivo);
                    var virtualPath = $"{ _configuration.GetValue<string>("VirtualPathBase") }/{ fileName }";

                    entity.CaminhoFisico = $"{ _basePath }/{ fileName }";
                    entity.CaminhoVirtual = virtualPath;
                }

                entity.DataEdicao = DateTime.UtcNow;
                _repository.Update(entity);
                
                return entity;
            }
            catch(Exception e)
            {
                throw new MidiaException("Não foi possível atualizar a mídia informada.", e);
            }
        }
    }
}
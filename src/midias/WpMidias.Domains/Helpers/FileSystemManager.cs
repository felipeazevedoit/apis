using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;
using WpMidias.Domains.Helpers.Exceptions;

namespace WpMidias.Domains.Helpers
{
    public class FileSystemManager
    {
        public async void SaveFile(string path, string fileName, byte[] file)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var filePath = $"{ path }{ fileName }";
                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Dispose();
                    await File.WriteAllBytesAsync(filePath, file);
                }
                else
                {
                    throw new FileSystemException("Arquivo já existente.");
                }
            }
            catch(FileSystemException e)
            {
                throw e;
            }
            catch(Exception e)
            {
                throw new FileSystemException("Não foi possível salvar o arquivo informado.", e);
            }
        }

        public void RemoveFile(string path, string fileName)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    var filePath = $"{ path }{ fileName }";
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }
            }
            catch (FileSystemException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new FileSystemException("Não foi possível salvar o arquivo informado.", e);
            }
        }

        public async void UpdateFile(string path, string fileName, byte[] file)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var filePath = $"{ path }{ fileName }";
                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Dispose();
                    await File.WriteAllBytesAsync(filePath, file);
                }
                else
                {
                    File.Delete(filePath);
                    File.Create(filePath).Dispose();
                    await File.WriteAllBytesAsync(filePath, file);
                }
            }
            catch (FileSystemException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new FileSystemException("Não foi possível salvar o arquivo informado.", e);
            }
        }

        public async Task<byte[]> GetFileAsync(string path, string fileName)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    throw new FileSystemException("Diretório informado não existe.");
                }
                
                var filePath = $"{ path }{ fileName }";
                if (!File.Exists(filePath))
                {
                    throw new FileSystemException("Arquivo informado não existe.");
                }

                var file = await File.ReadAllBytesAsync(filePath);
                return file;
            }
            catch(FileSystemException e)
            {
                throw e;
            }
            catch(Exception e)
            {
                throw new FileSystemException("Não foi possível recuperar o arquivo solicitado.", e);
            }
        }
    }
}

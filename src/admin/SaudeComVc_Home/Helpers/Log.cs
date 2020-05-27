using SaudeComVc_Home.Models;
using SaudeComVoce.Helpers;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace SaudeComVc_Home.Helpers
{
    public static class Log
    {
        public static async Task<LogModel> GerarLogAsync(string controllerName, string controllerAction, string baseUrl)
        {
            try
            {
                var log = new LogModel()
                {
                    Descricao = controllerName,
                    DataCriacao = DateTime.Now,
                    UsuarioCriacao = PixCoreValues.UsuarioLogado == null ? 0 : PixCoreValues.UsuarioLogado.IdUsuario,
                    Ativo = true,
                    IdCliente = 12,
                    Status = 1,
                    UrlAcessada = $"{ baseUrl }/{ controllerAction }",
                };

                var envio = new
                {
                    log,
                };

                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<LogModel>(keyUrl, $"/Seguranca/WpLogs/SalvarLog/{ log.IdCliente }/999", envio);

                return result;
            }
            catch(Exception e)
            {
                return new LogModel();
            }
        }
    }
}
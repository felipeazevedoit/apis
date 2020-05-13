using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WpFinanceiro.Entities;
using WpFinanceiro.Services;

namespace WpFinanceiro.Helpers
{
    public class EmailHandler
    {
        private readonly IConfiguration _config;
        private readonly ConfiguracaoService _configService;
        private readonly EmailService _emailService;
        private readonly SegurancaService _service;

        public EmailHandler(IConfiguration config, ConfiguracaoService configService, EmailService emailService, SegurancaService service)
        {
            _config = config;
            _configService = configService;
            _emailService = emailService;
            _service = service;
        }

        public async Task EnviaEmailAsync(string token, Extrato extrato)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var (emailConfigs, emailConstants) = GetConfiguration();

                var content = await System.IO.File.ReadAllTextAsync("wwwroot/Credito.html");

                foreach (var item in emailConstants)
                {
                    var text = extrato.GetType().GetProperty(item.Key).GetValue(extrato, null).ToString();

                    if (!string.IsNullOrEmpty(text))
                        content = content.Replace(item.Value, text);
                }

                var configuracoes = await _configService.GetConfiguracoesAsync(extrato.IdCliente, extrato.UsuarioCriacao);
                var sender = emailConfigs.GetValue<string>("Sender");

                var configuracao = configuracoes.Where(c => c.Chave.Equals(sender)).SingleOrDefault();

                if (!string.IsNullOrEmpty(extrato.EmailEmpresa))
                {
                    var emailToClient = new Email(content, "Crédito em conta StaffPro", configuracao.Valor, extrato.EmailEmpresa, extrato.IdCliente);
                    await _emailService.EnviarEmailAsync(emailToClient, extrato.IdCliente, extrato.UsuarioCriacao);
                }
            }
            catch (Exception e)
            {
                //Erro ao enviar e-mail não impacta no processo
            }
        }

        private (IConfigurationSection emailConfigs,
            IEnumerable<IConfigurationSection> emailConstants) GetConfiguration()
        {
            var emailConfigs = _config.GetSection("EmailSettings");
            var emailConstants = emailConfigs.GetSection("Constants").GetChildren();

            return (emailConfigs, emailConstants);
        }
    }
}

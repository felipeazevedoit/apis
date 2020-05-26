using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using RestSharpEx;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPixPrincipalAPI.Model;
using WebPixPrincipalRepository.Entity;

namespace WebPixPrincipalAPI.Helper
{
    public class EmailHandler
    {
        private readonly RemetenteObject _remetente;

        public EmailHandler(RemetenteObject remetente)
        {
            _remetente = remetente;
        }

        public async Task EnviarEmailSenhaAsync(WebPixPrincipalRepository.Entity.Usuario usuario)
        {
            try
            {
                RestClient client = new RestClient("http://webmail.talanservices.com.br/");
                var url = "api/Enviar";
                RestRequest request = null;
                request = new RestRequest(url, Method.POST);

                var destinatario = new EmailModel(usuario.Nome, usuario.Login);
                var remetenteEmail = _remetente.Email;
                var remetente = new EmailModel("Atendimento", remetenteEmail);

                var html = File.ReadAllText("wwwroot/NovaSenha.html");
                html = html.Replace("USUARIO_NOME", usuario.Nome);
                html = html.Replace("USUARIO_LOGIN", usuario.Login);
                html = html.Replace("USUARIO_SENHA", usuario.Senha);
                
                if(usuario.idCliente == 12) //Saude Com Vc 
                {
                    html = html.Replace("BUTTON_LOGIN", "<a href='http://saudecomvc.talanservices.com.br/'><button type='button' class='button efeito efeito-3'>Efetuar Login</button></a>");
                    html = html.Replace("COPYRIGHT", "Copyright © 2019 - SAÚDE COM VC. Todos os direitos reservados.");
                    html = html.Replace("LOGO", "<h3>< img class='logo' src='http://talanservices.com.br/SaudeComVc.png'/></h3>");
                }
                else if (usuario.idCliente == 1) //StaffPro
                {
                    html = html.Replace("BUTTON_LOGIN", string.Empty);
                    html = html.Replace("COPYRIGHT", "Copyright © 2019 - StaffPro. Todos os direitos reservados.");
                    html = html.Replace("LOGO", string.Empty);
                }
                else
                {
                    html = html.Replace("BUTTON_LOGIN", string.Empty);
                    html = html.Replace("COPYRIGHT", "string.Empty");
                    html = html.Replace("LOGO", string.Empty);
                }

                var mail = new Mail()
                {
                    Assunto = $"Recuperação de senha - { usuario.Nome }",
                    Remetente = remetente,
                    Html = true,
                    Mensagem = html,
                    Destinatarios = new List<EmailModel>() { destinatario },
                };

                var jsonToSend = JsonConvert.SerializeObject(mail);

                request.AddParameter("application/json; charset=utf-8", jsonToSend, ParameterType.RequestBody);
                var response = await client.ExecuteTaskAsync(request);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task EnviarEmailAsync(WebPixPrincipalRepository.Entity.Usuario usuario)
        {
            try
            {
                RestClient client = new RestClient("http://webmail.talanservices.com.br/");
                var url = "api/Enviar";
                RestRequest request = null;
                request = new RestRequest(url, Method.POST);

                var destinatario = new EmailModel(usuario.Nome, usuario.Login);
                var remetenteEmail = _remetente.Email;
                var remetente = new EmailModel("Atendimento", remetenteEmail);

                var html = File.ReadAllText("wwwroot/TemplateCadastro.html");
                html = html.Replace("USUARIO_NOME", usuario.Nome);
                html = html.Replace("USUARIO_LOGIN", usuario.Login);

                var mail = new Mail()
                {
                    Assunto = $"Cadastro - { usuario.Nome }",
                    Remetente = remetente,
                    Html = true,
                    Mensagem = html,
                    Destinatarios = new List<EmailModel>() { destinatario },
                };

                var jsonToSend = JsonConvert.SerializeObject(mail);

                request.AddParameter("application/json; charset=utf-8", jsonToSend, ParameterType.RequestBody);
                var response = await client.ExecuteTaskAsync(request);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task EnviarEmailUlabelAsync(WebPixPrincipalRepository.Entity.Usuario usuario)
        {
            try
            {
                RestClient client = new RestClient("http://webmail.talanservices.com.br/");
                var url = "api/Enviar";
                RestRequest request = null;
                request = new RestRequest(url, Method.POST);

                var destinatario = new EmailModel(usuario.Nome, usuario.Login);
                var remetenteEmail = _remetente.Email;
                var remetente = new EmailModel("Atendimento", remetenteEmail);

                var html = File.ReadAllText("wwwroot/Index.html");
                html = html.Replace("NOME_USUARIO", usuario.Nome);

                var mail = new Mail()
                {
                    Assunto = $"Cadastro - { usuario.Nome }",
                    Remetente = remetente,
                    Html = true,
                    Mensagem = html,
                    Destinatarios = new List<EmailModel>() { destinatario },
                };

                var jsonToSend = JsonConvert.SerializeObject(mail);

                request.AddParameter("application/json; charset=utf-8", jsonToSend, ParameterType.RequestBody);
                var response = await client.ExecuteTaskAsync(request);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task EnviarEmail(int tipo, int entityId, IEnumerable<Usuario> admins)
        {
            try
            {
                RestClient client = new RestClient("http://webmail.talanservices.com.br/");
                var url = "api/Enviar";
                RestRequest request = null;
                request = new RestRequest(url, Method.POST);

                //var destinatario = new EmailModel(usuario.Nome, usuario.Login);
                var remetenteEmail = _remetente.Email;
                var remetente = new EmailModel("Atendimento", remetenteEmail);

                var html = File.ReadAllText("wwwroot/TemplateSaude.html");

                switch (tipo)
                {
                    case 1:
                        html = html.Replace("TEXTO_CADASTRO", "Uma nova clínica");
                        break;
                    case 2:
                        html = html.Replace("TEXTO_CADASTRO", "Um novo Fornecedor");
                        break;
                    case 3:
                        html = html.Replace("TEXTO_CADASTRO", "Um novo Médico");
                        break;
                    case 4:
                        html = html.Replace("TEXTO_CADASTRO", "Um novo Profissional");
                        break;
                }

                var guid = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{ tipo }+{ entityId }"));

                html = html.Replace("TEXTO_CADASTRO", guid);

                var destinatarios = new List<EmailModel>();

                foreach (var item in admins)
                {
                    destinatarios.Add(new EmailModel(item.Nome, item.Login));
                }

                var mail = new Mail()
                {
                    Assunto = $"Cadastro - Saúde Com Vc",
                    Remetente = remetente,
                    Html = true,
                    Mensagem = html,
                    Destinatarios = destinatarios,
                };

                var jsonToSend = JsonConvert.SerializeObject(mail);

                request.AddParameter("application/json; charset=utf-8", jsonToSend, ParameterType.RequestBody);
                var response = await client.ExecuteTaskAsync(request);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

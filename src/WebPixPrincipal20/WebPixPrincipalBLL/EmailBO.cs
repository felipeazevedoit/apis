using System;
using WebPixPrincipalRepository;
using WebPixPrincipalRepository.Entity;
using MailKit.Net.Smtp;
using MimeKit;
using RestSharp;
using RestSharpEx;
using System.IO;
using RestSharp.Authenticators;

namespace WebPixPrincipalBLL
{
    public class EmailBO
    {       

        public async System.Threading.Tasks.Task<bool> EnviaSimplesEmailAsync(Email email, string remetente, string destinatario, int idCliente)
        {
            var paramentros = ConfiguracaoDAO.GetParametros(idCliente);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Contato", remetente));
            message.To.Add(new MailboxAddress(destinatario));
            message.Subject = email.Titulo;

            message.Body = new TextPart("plain")
            {
                Text = email.Conteudo
            };

            try
            {
                RestClient client = new RestClient();
                client.BaseUrl = new Uri("https://api.mailgun.net/v3");
                client.Authenticator =
                    new HttpBasicAuthenticator("api",
                                                "34f01503b964fc6058eb634dbe2b0600-9525e19d-57d02faf");
                RestRequest request = new RestRequest();
                request.AddParameter("domain", "mundowebpix.com.br", ParameterType.UrlSegment);
                request.Resource = "{domain}/messages";
                request.AddParameter("from", "Site <mailgun@mundowebpix.com.br>");
                request.AddParameter("to", destinatario);
                request.AddParameter("subject", email.Titulo);
                request.AddParameter("text", email.Conteudo);
                //request.AddParameter("html", "<html><p>Put html here.</p></html>");
                request.Method = Method.POST;
                var response = await client.ExecuteTaskAsync(request);
                return true;
            }
            catch(Exception e)
            {
                string fileName = @"D:\Web\Logs\logs.txt";
                using (StreamWriter sw = File.AppendText(fileName))
                {
                    sw.WriteLine("--------- Append Text Start ----------");
                    sw.WriteLine(e.Message);
                    sw.WriteLine("--------- Append Text End ----------");
                }
                return false;
            }
            
        }

    }
}

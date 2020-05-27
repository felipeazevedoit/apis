using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.IO;

namespace TServices.Comum.Notificacao.Email
{
    public class EmailService
    {
        public static async Task<bool> Send(string host, int port, bool EnableSsl, bool useCredential, string user,
                string password, bool isBodyHtml, string remetente, string to, string subject, string body, string cc = null, string cco = null,
                Dictionary<string, string> anexos = null)
        {
            try
            {
                using (System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient())
                {
                    smtp.Host = host;
                    smtp.Port = port;
                    smtp.EnableSsl = EnableSsl;
                    smtp.UseDefaultCredentials = useCredential;
                    smtp.Credentials = new System.Net.NetworkCredential(user, password);

                    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

                    mail.From = new System.Net.Mail.MailAddress(remetente);
                    mail.IsBodyHtml = isBodyHtml;

                    if (!string.IsNullOrWhiteSpace(to))
                        to.Split(';')
                            .ToList()
                            .ForEach(email =>
                            {
                                mail.To.Add(new System.Net.Mail.MailAddress(email));
                            });

                    if (!string.IsNullOrWhiteSpace(cc))
                        cc.Split(';')
                            .ToList()
                            .ForEach(email =>
                            {
                                mail.CC.Add(new System.Net.Mail.MailAddress(email));
                            });

                    if (!string.IsNullOrWhiteSpace(cco))
                        cco.Split(';')
                            .ToList()
                            .ForEach(email =>
                            {
                                mail.Bcc.Add(new System.Net.Mail.MailAddress(email));
                            });

                    mail.Subject = subject;

                    anexos?
                      .ToList()
                      .ForEach(arq =>
                      {
                          string base64String = System.Uri.UnescapeDataString(arq.Value);
                          byte[] arrayFile = Convert.FromBase64String(base64String);

                          Attachment anexado = new Attachment(new MemoryStream(arrayFile), arq.Key);

                          mail.Attachments.Add(anexado);
                      });

                    if (!string.IsNullOrWhiteSpace(body))
                    {
                        mail.Body = body;
                    }

                    await smtp.SendMailAsync(mail);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

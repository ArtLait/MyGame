using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using System.Web.Configuration;
using System;

namespace MyWebGam.Service
{
    public class EmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();
            int portServer = Int32.Parse(WebConfigurationManager.AppSettings["port"]);
            string emailServer = WebConfigurationManager.AppSettings["email"];
            string smtpServer = WebConfigurationManager.AppSettings["smtp"];
            string passwordEmailServer = WebConfigurationManager.AppSettings["passwordEmail"];
            emailMessage.From.Add(new MailboxAddress("Администрация сайта", emailServer));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(smtpServer, portServer, false);
                await client.AuthenticateAsync(emailServer, passwordEmailServer);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
            
        }
        public static string Href(string url)
        {
            return string.Format("Для завершения регистрации перейдите по ссылке:" +
                            "<a href=\"{0}\" title=\"Подтвердить регистрацию\">{0}</a>",
                url);
        }
    }
}
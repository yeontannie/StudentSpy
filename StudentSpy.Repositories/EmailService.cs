//using MimeKit;
//using MailKit.Net.Smtp;
//using System.Net.Http;
//using System.Net.Mail;
//using SendGrid.Helpers.Mail;
//using SendGrid;
//using Microsoft.Extensions.Configuration;
//using System.Text;
using Microsoft.AspNetCore.Http;
using MimeKit;
using System.Net;
using System.Net.Mail;

namespace StudentSpy.Repositories
{
    public class EmailService : IEmailService
    {//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
        public void SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MailMessage();

            emailMessage.From = new MailAddress("studentspy35@gmail.com");
            emailMessage.To.Add(email);
            emailMessage.Subject = subject;
            emailMessage.Body = message;
            emailMessage.IsBodyHtml = true;

            using (var client = new SmtpClient())
            {
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("studentspy35@gmail.com", "Password!!11");
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                try
                {
                    client.Send(emailMessage);
                }
                catch(Exception e)
                {
                    
                }
                
                //client.ConnectAsync("smtp.gmail.com", 587, true);
                //client.AuthenticateAsync("studentspy35@gmail.com", "Password!!11");
                //client.SendAsync(emailMessage);

                //client.DisconnectAsync(true);
            }

            //Environment.SetEnvironmentVariable("Api_Key", "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");
            //var apiKey = Environment.GetEnvironmentVariable("Api_Key");
            //var client = new SendGridClient(apiKey);
            //var from = new EmailAddress("tanastasuk35@gmail.com", "Site Administration");
            //var to = new EmailAddress(email, "");
            //var msg = MailHelper.CreateSingleEmail(
            //    from, to, subject, "Dear User!", message);
            //client.SendEmailAsync(msg).Wait();
        }
    }
}

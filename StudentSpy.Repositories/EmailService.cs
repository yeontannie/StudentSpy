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
    {
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
            }
        }
    }
}

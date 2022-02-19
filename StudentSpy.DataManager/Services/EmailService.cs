using MimeKit;
using MailKit.Net.Smtp;
using MimeKit.Text;

namespace StudentSpy.DataManager.Services
{
    public class EmailService : IEmailService
    {//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
        public void SendEmail(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(MailboxAddress.Parse("studentspy35@gmail.com"));
            emailMessage.To.Add(MailboxAddress.Parse(email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(TextFormat.Html)
            {
                Text = message
            };

            using var client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587);

            // Note: since we don't have an OAuth2 token, disable
            // the XOAUTH2 authentication mechanism.
            client.AuthenticationMechanisms.Remove("XOAUTH2");

            client.Authenticate("studentspy35@gmail.com", "Password!!11");
            client.Send(emailMessage);

            client.Disconnect(true);
        }
    }
}
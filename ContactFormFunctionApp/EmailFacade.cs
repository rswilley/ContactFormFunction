using ContactFormFunctionApp.Config;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace ContactFormFunctionApp
{
    public class EmailFacade
    {
        public static void SendEmail(SendEmailInput input, Configuration config)
        {
            var email = SetupEmailMessage(input, config);

            using (var client = new SmtpClient(config.EmailHost, Convert.ToInt32(config.EmailPort)))
            {
                client.Credentials = new NetworkCredential(config.SmtpUsername, config.SmtpPassword);
                client.EnableSsl = true;

                client.Send(email);
            }
        }

        private static MailMessage SetupEmailMessage(SendEmailInput input, Configuration config)
        {
            var email = new MailMessage
            {
                IsBodyHtml = true,
                From = new MailAddress(config.FromEmail, config.FromName)
            };

            var sendToEmailAddress = config.ValidDomains
                .Single(d => d.DomainName == input.FromDomain).SendToEmail;

            email.To.Add(new MailAddress(sendToEmailAddress));
            email.ReplyToList.Add(input.Email);
            email.Subject = config.SubjectPrefix + input.Subject;
            email.Body = $"<p>{input.Message}</p>";

            return email;
        }
    }
}

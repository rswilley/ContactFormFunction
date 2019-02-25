using System;
using System.Net;
using System.Net.Mail;

namespace ContactFormFunctionApp
{
    public class EmailFacade
    {
        public static void SendEmail(SendEmailInput input, Configuration config)
        {
            var email = new MailMessage
            {
                IsBodyHtml = true,
                From = new MailAddress(config.FromEmail, config.FromName)
            };

            email.To.Add(new MailAddress(config.SendToEmail));
            email.ReplyToList.Add(input.Email);
            email.Subject = config.SubjectPrefix + input.Subject;
            email.Body = $"<p>{input.Message}</p>";

            using (var client = new SmtpClient(config.EmailHost, Convert.ToInt32(config.EmailPort)))
            {
                client.Credentials = new NetworkCredential(config.SmtpUsername, config.SmtpPassword);
                client.EnableSsl = true;

                client.Send(email);
            }
        }
    }
}

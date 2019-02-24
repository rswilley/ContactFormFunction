using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Http;

namespace ContactFormFunctionApp
{
    public static class SendEmailFunction
    {
        [FunctionName("SendEmail")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]HttpRequest req, TraceWriter log)
        {
            log.Info("SendEmail HTTP trigger function processed a request.");

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            var input = JsonConvert.DeserializeObject<SendEmailInput>(requestBody);

            var wasSent = await SendEmail(input);
            if (wasSent)
                return new OkResult();
            else
                return new InternalServerErrorResult();
        }

        private static readonly string emailHost = Environment.GetEnvironmentVariable("EmailHost");
        private static readonly string emailPort = Environment.GetEnvironmentVariable("EmailPort");
        private static readonly string smtpUsername = Environment.GetEnvironmentVariable("SmtpUsername");
        private static readonly string smtpPassword = Environment.GetEnvironmentVariable("SmtpPassword");
        private static readonly string subjectPrefix = Environment.GetEnvironmentVariable("SubjectPrefix");
        private static readonly string sendToEmail = Environment.GetEnvironmentVariable("SendToEmail");

        private class SendEmailInput
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Subject { get; set; }
            public string Message { get; set; }
        }

        private static Task<bool> SendEmail(SendEmailInput input)
        {
            var email = new MailMessage
            {
                IsBodyHtml = true,
                From = new MailAddress("mail@econtactform.com")
            };

            email.To.Add(new MailAddress(sendToEmail));
            email.ReplyToList.Add(input.Email);
            email.Subject = subjectPrefix + input.Subject;
            email.Body = $"<p>{input.Message}</p>";

            using (var client = new SmtpClient(emailHost, Convert.ToInt32(emailPort)))
            {
                client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                client.EnableSsl = true;

                try
                {
                    client.Send(email);
                    return Task.FromResult(true);
                }
                catch (Exception)
                {
                    return Task.FromResult(false);
                }
            }
        }
    }
}

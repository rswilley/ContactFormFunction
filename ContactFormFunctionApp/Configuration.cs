using System;

namespace ContactFormFunctionApp
{
    public class Configuration
    {
        public string EmailHost => Environment.GetEnvironmentVariable("EmailHost");
        public string EmailPort => Environment.GetEnvironmentVariable("EmailPort");
        public string SmtpUsername => Environment.GetEnvironmentVariable("SmtpUsername");
        public string SmtpPassword => Environment.GetEnvironmentVariable("SmtpPassword");
        public string FromEmail => Environment.GetEnvironmentVariable("FromEmail");
        public string FromName => Environment.GetEnvironmentVariable("FromName");
        public string SubjectPrefix => Environment.GetEnvironmentVariable("SubjectPrefix");
        public string SendToEmail => Environment.GetEnvironmentVariable("SendToEmail");
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace ContactFormFunctionApp.Config
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

        public IEnumerable<ValidDomain> ValidDomains => GetValidDomains();

        private static IEnumerable<ValidDomain> GetValidDomains()
        {
            var validDomainsValue = Environment.GetEnvironmentVariable("ValidDomains");

            var validDomains = validDomainsValue.Split(';');
            return validDomains.Select(s =>
            {
                var value = s.Split('|');

                return new ValidDomain
                {
                    DomainName = value[0],
                    SendToEmail = value[1]
                };
            });
        }
    }
}

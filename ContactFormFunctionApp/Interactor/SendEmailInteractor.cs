using ContactFormFunctionApp.Config;
using ContactFormFunctionApp.Interactor;
using System.Linq;

namespace ContactFormFunctionApp
{
    public class SendEmailInteractor
    {
        public SendEmailResponse SendEmail(SendEmailInput input)
        {
            var config = new Configuration();
            var isValidRequest = IsValidRequest(input, config);

            if (isValidRequest)
            {
                EmailFacade.SendEmail(input, config);

                return new SendEmailResponse
                {
                    IsRequestValid = true
                };
            }

            return new SendEmailResponse
            {
                IsRequestValid = false
            };
        }

        private bool IsValidRequest(SendEmailInput input, Configuration config)
        {
            if (string.IsNullOrEmpty(input.FromDomain) ||
                string.IsNullOrEmpty(input.Name) ||
                string.IsNullOrEmpty(input.Email) ||
                string.IsNullOrEmpty(input.Subject) ||
                string.IsNullOrEmpty(input.Message))
                return false;

            return config.ValidDomains
                .Select(d => d.DomainName)
                .Contains(input.FromDomain);
        }
    }
}

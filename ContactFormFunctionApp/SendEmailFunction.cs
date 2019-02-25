using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System;
using System.IO;

namespace ContactFormFunctionApp
{
    public static class SendEmailFunction
    {
        [FunctionName("SendEmail")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]HttpRequest req, TraceWriter log)
        {
            log.Info("SendEmail HTTP trigger function processed a request.");

            try
            {
                var requestBody = new StreamReader(req.Body).ReadToEnd();
                var input = JsonConvert.DeserializeObject<SendEmailInput>(requestBody);

                if (!IsValidateInput(input))
                    return new BadRequestResult();

                var config = new Configuration();
                EmailFacade.SendEmail(input, config);

                return new OkResult();
            }
            catch (Exception e)
            {
                log.Error("SendEmail HTTP trigger function threw an error.", e);
                throw;
            }
        }

        private static bool IsValidateInput(SendEmailInput input)
        {
            if (string.IsNullOrEmpty(input.Name) || 
                string.IsNullOrEmpty(input.Email) ||
                string.IsNullOrEmpty(input.Subject) || 
                string.IsNullOrEmpty(input.Message))
                return false;

            return true;
        }
    }
}

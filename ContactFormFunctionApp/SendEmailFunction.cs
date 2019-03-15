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

                var interactor = new SendEmailInteractor();
                var response = interactor.SendEmail(input);

                if (response.IsRequestValid)
                    return new OkResult();

                return new BadRequestResult();
            }
            catch (Exception e)
            {
                log.Error("SendEmail HTTP trigger function threw an error.", e);
                throw;
            }
        }
    }
}

using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage.Table;
using System.Threading;
using System.Linq;
using pv207_services.Models;

namespace pv207_services
{
    public static class PaymentFunctions
    {
        [FunctionName("MakePayment")]
        public static async Task<IActionResult> MakePayment(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "payment")] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            log.LogInformation($"Processing a payment");

            var input = JsonConvert.DeserializeObject<CreditCardModel>(requestBody);

            if (input.Number == "4111 1111 1111 1111" && input.Expiration == "10/21" && input.Cvc == 123)
                return new OkResult();

            return new ForbidResult();
        }
    }
}

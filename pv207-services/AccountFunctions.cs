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
    public static class AccountFunctions
    {
        [FunctionName("CreateAccount")]
        public static async Task<IActionResult> CreateAccount(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "accounts")] HttpRequest req,
            ILogger log)
        {
            return new OkResult();
        }
    }
}

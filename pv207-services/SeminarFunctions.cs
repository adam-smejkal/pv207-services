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
    public static class Functions
    {
        [FunctionName("GetSeminars")]
        public static async Task<IActionResult> GetSeminars(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "seminars")] HttpRequest req,
            [Table("Seminars")] CloudTable table,
            ILogger log)
        {
            if (req.Query.TryGetValue("lookup", out var lookup))
            {
                var condition = TableQuery.GenerateFilterCondition("Name", QueryComparisons.Equal, lookup);
                var results = await table.ExecuteQueryAsync(new TableQuery<SeminarTableEntity>().Where(condition), CancellationToken.None);
                if (!results.Any())
                    return new NotFoundResult();

                return new OkObjectResult(results.First().ToModel());
            }

            throw new NotImplementedException();
        }

        [FunctionName("CreateSeminar")]
        public static async Task<IActionResult> CreateSeminar(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "seminars")] HttpRequest req,
            [Table("Seminars")] CloudTable table,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<SeminarCreateModel>(requestBody);

            var entity = new Seminar() 
            { 
                Id = Guid.NewGuid().ToString(), 
                Name = input.Name,
                Information = input.Information,
                Duration = input.Duration,
                StartDate = input.StartDate
            };

            await table.ExecuteAsync(TableOperation.Insert(entity.ToTableEntity()));

            return new OkObjectResult(entity);
        }

        [FunctionName("UpdateSeminar")]
        public static async Task<IActionResult> UpdateSeminar(
            [HttpTrigger(AuthorizationLevel.Anonymous, "patch", Route = "seminars/{id}")] HttpRequest req,
            [Table("Seminars")] CloudTable table,
            ILogger log,
            string id)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<SeminarUpdateModel>(requestBody);

            var filter = TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, id);
            var result = await table.ExecuteQueryAsync(new TableQuery<SeminarTableEntity>().Where(filter));
            if (!result.Any())
                return new NotFoundResult();

            var original = result.First().ToModel();

            var entity = new Seminar()
            {
                Id = original.Id,
                Name = input.Name ?? original.Name,
                Information = input.Information ?? original.Information,
                Duration = input.Duration ?? original.Duration,
                StartDate = input.StartDate ?? original.StartDate
            };

            await table.ExecuteAsync(TableOperation.Replace(entity.ToTableEntity()));

            return new OkObjectResult(entity);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using pv207_services.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace pv207_services
{
    public static class LectureFunctions
    {
        [FunctionName("GetLectures")]
        public static async Task<IActionResult> GetLectures(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "lectures")] HttpRequest req,
            [Table("Lectures")] CloudTable table,
            ILogger log)
        {
            if (req.Query.TryGetValue("seminar", out var seminar) && req.Query.TryGetValue("day", out var day))
            {
                log.LogInformation($"Looking up available lectures for day {day} of seminar {seminar}");

                var results = await table.ExecuteQueryAsync(new TableQuery<SeminarTableEntity>(), CancellationToken.None);
                if (!results.Any())
                    return new NotFoundResult();

                var result = new LectureSearchResultModel();
                result.Items = results.Select(x => new LectureSearchResultModel.LectureSearchResultItem() { Id = x.Id, Name = x.Name }).ToList();

                return new OkObjectResult(result);
            }

            throw new NotImplementedException();
        }

        [FunctionName("CreateLecture")]
        public static async Task<IActionResult> CreateLecture(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "lectures")] HttpRequest req,
            [Table("Lectures")] CloudTable table,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            log.LogInformation($"Creating a lecture: {requestBody}");

            var input = JsonConvert.DeserializeObject<LectureCreateModel>(requestBody);

            var entity = new Lecture()
            {
                Id = Guid.NewGuid().ToString(),
                Name = input.Name,
                Information = input.Information
            };

            await table.ExecuteAsync(TableOperation.Insert(entity.ToTableEntity()));

            return new OkObjectResult(entity);
        }
    }
}

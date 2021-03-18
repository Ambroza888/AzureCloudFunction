using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Valence.Models;
using Valence.Extensions;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Valence
{
    public static class business
    {
        private static string YELP_API_KEY = DirectoryExtentions.GetLocalSettingJson("ApiKey");
        private static string YELP_BASE_URL = DirectoryExtentions.GetLocalSettingJson("YELP_BASE_URL");

        [FunctionName("business")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {

            // log.LogInformation(YELP_BASE_URL);
            // log.LogInformation(YELP_API_KEY);
            // return new OkObjectResult("hi");


            var QueryParam = new LocationParams();
            QueryParam.Location = req.Query["location"];
            QueryParam.Categories = req.Query["categories"];

            if (QueryParam.Location == null || QueryParam.Categories == null)
            {
                return new OkObjectResult((QueryParam.Location != null)
                    ? $"Location is valid, Please add categories in the query string"
                    : $"Categories is valid, Please add Location in the query string");
            }
            else
            {

                return new OkObjectResult(QueryParam);
            }




            // log.LogInformation("C# HTTP trigger function processed a request.");
            // string name = req.Query["name"];
            // string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            // dynamic data = JsonConvert.DeserializeObject(requestBody);
            // name = name ?? data?.name;
            // string responseMessage = string.IsNullOrEmpty(name)
            //     ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
            //     : $"Hello, {name}. This HTTP triggered function executed successfully.";
            // return new OkObjectResult(responseMessage);
        }
    }
}

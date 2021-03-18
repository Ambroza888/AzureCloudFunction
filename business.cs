using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Valence.Models;
using System.Net.Http;
using Valence.Helper;

namespace Valence
{
    public static class business
    {
        [FunctionName("business")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            var QueryParam = new LocationParams();
            QueryParam.Location = req.Query["location"];
            QueryParam.Categories = req.Query["categories"];

            if (QueryParam.Location == null || QueryParam.Categories == null)
            {
                // the good way of handling that response is,
                // depends on how the client want the date to be shape like.
                return new OkObjectResult((QueryParam.Location != null)
                    ? $"Location is valid, Please specify categories"
                    : $"Categories are valid, Please specify Location in the query string");
            }
            else
            {
                try
                {
                    var YelpResponse = await Agent.GetYelpApi(QueryParam.Location, QueryParam.Categories);

                    YelpParams response = JsonConvert.DeserializeObject<YelpParams>(YelpResponse);

                    var data = GenerateList.Generate(response);

                    return new OkObjectResult(data);
                }
                catch(HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ",e.Message);

                    return new BadRequestObjectResult("Problem retreving data from API");
                }
            }
        }
    }
}

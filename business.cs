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
using Valence.Validation;

namespace Valence
{
    public static class business
    {
        [FunctionName("business")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            var QueryParam = ValidateQueryRequest.Validate(req.Query);

            if (QueryParam == null)
            {
                return new OkObjectResult("Please specify a location or a latitude and longitude");
            }
            else
            {
                try
                {
                    var YelpResponse = await Agent.GetYelpApi(QueryParam.Location, QueryParam.Categories);

                    YelpParams response = JsonConvert.DeserializeObject<YelpParams>(YelpResponse);

                    var BusinessesToReturn = GenerateList.Generate(response);

                    return new OkObjectResult(BusinessesToReturn);

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

using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Linq;
using LuhnAlgorithm_AzureAPI.ApiVersions.v1.ApiClasses;

namespace LuhnAlgorithm_AzureAPI.Classes.v1.Luhn
{
    public class LuhnAlgorithmAPI
    {
        private readonly ILogger<LuhnAlgorithmAPI> _logger;

        public LuhnAlgorithmAPI(ILogger<LuhnAlgorithmAPI> log)
        {
            _logger = log;
        }

        [FunctionName("CheckLuhnCalulation")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "value" })]
        [OpenApiParameter(name: "value", In = ParameterLocation.Query, Required = true, Type = typeof(ApiRequests_CheckLuhnCalculation), Description = "Value to calculate Luhn on. Only accepts 0-9, '-' will be removed")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "json", bodyType: typeof(ApiResponses_CheckLuhnCalculation), Description = "Returns the sent value, result. If failed a corrected Luhn is returned")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/Luhn/CheckLuhnCalulation")] HttpRequest req, ExecutionContext executionContext)
        {
            _logger.LogInformation($"'{executionContext.FunctionName}' Request received.");
            //Constructor
            string value;

            //Check if we have the data as a query
            value = req.Query["value"];

            //If not, check if we have it in the body
            if(string.IsNullOrEmpty(value))
            {
                //Check body for data
                var requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                //Add to parameter
                dynamic data = JsonConvert.DeserializeObject(requestBody);
                value = value ?? data?.value;
            }

            //We should have value here, if not return error
            if(string.IsNullOrEmpty(value))
            {
                return new BadRequestObjectResult(new ApiResponses_BadRequest() { message = "Parameter / Body data 'value' is missing" });
            }
            else if(value.Replace("-","").All(char.IsDigit))
            {
                return new BadRequestObjectResult(new ApiResponses_BadRequest() { message = "Parameter / Body data 'value' contains none valid data. Only '0' to '9' are valid characters." });
            }
            else
            {
                //Constructor
                var response = new ApiResponses_CheckLuhnCalculation() { value = value };

                //Check Luhn calculation
                if(await CheckLuhnCalculation(value))
                {
                    //Correct, set valid values.
                    response.result = true;
                }
                else
                {
                    //Faulty, Calculate correct data
                    response.result = false;
                    response.correctedLuhn = await CalculateCorrectLuhnNumber(value);
                }

                //Calculation went wrong but both are valid results
                return new OkObjectResult(response);
            }
        }

        public async Task<bool> CheckLuhnCalculation(string inputValue)
        {
             return true;
        }

        public async Task<int> CalculateCorrectLuhnNumber(string inputValue)
        {
            return 1;
        }
    }
}


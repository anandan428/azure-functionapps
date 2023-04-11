using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace FunctionApp
{
    public class ApplicationInsights
    {
        private readonly ILogger<ApplicationInsights> _logger;

        public ApplicationInsights(ILogger<ApplicationInsights> logger)
        {
            _logger = logger;
        }

        [Function("TestApplicationInsights")]
        public async Task<HttpResponseData> TestApplicationInsights(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req, 
            FunctionContext context)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            
            await response.WriteAsJsonAsync<Response<string>>(
                new Response<string>() {
                    Message = "Appinsights test successfull",
                    Data = context.FunctionId
                }
            );

            return response;
        }
    }
}

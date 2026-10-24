using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebAppFunction.Api.Services;
using WebAppFunction.Api.Services.Interfaces;

namespace WebAppFunction.Api.Functions
{
    public class MsGraph
    {
        private readonly IGraphService _graphService;

        public MsGraph(IGraphService graphService)
        {
            _graphService = graphService;
        }

        [FunctionName("GetAzureUsers")]
        public async Task<IActionResult> GetAzureUsers(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "AzureUsers")]
        HttpRequest req,
        ILogger log
    )
        {
            try
            {
                var users = await _graphService.Users();
                return new JsonResult(users);
            }
            catch (Exception ex)
            {
                return HandleException(nameof(this.GetAzureUsers), ex, log);
            }
        }

        private IActionResult HandleException(string funcName, Exception ex, ILogger log)
        {
            if (ex is UnauthorizedAccessException)
                return new ObjectResult(ex.Message) { StatusCode = StatusCodes.Status401Unauthorized };

            log.LogError(ex, $"Error in {funcName}");
            return new ObjectResult(ex.Message) { StatusCode = StatusCodes.Status500InternalServerError };
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace WebAppFunction.Api.Functions
{
  public static class HealthCheck
  {
    [FunctionName("HealthCheck")]
    public static IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "health-check")]
            HttpRequest req,
        ILogger log
    )
    {
      return new OkResult();
    }
  }
}

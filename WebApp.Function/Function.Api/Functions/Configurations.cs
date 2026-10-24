using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebAppFunction.Api.Models;
using WebAppFunction.Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace WebAppFunction.Api.Functions;

public class Configurations
{
  private readonly IConfigurationService _configurationService;
  private readonly IMapper _mapper;

  public Configurations(
      IConfigurationService configurationService,
      IMapper mapper
  )
  {
    _configurationService = configurationService;
    _mapper = mapper;
  }

  [FunctionName("GetConfig")]
  public async Task<IActionResult> GetConfigurations(
      [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Configuration")]
        HttpRequest req,
      ILogger log
  )
  {
    return new JsonResult(await _configurationService.GetConfigurationModelAsync());
  }

  [FunctionName("UpdateConfig")]
  public async Task<IActionResult> UpdateConfigurations(
      [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Configuration")]
        HttpRequest req,
      ILogger log
  )
  {
    try
    {
      var authModel = _mapper.Map<AuthModel>(req);
      if (!authModel.Roles.Contains(AuthRoles.Supervisor))
        throw new UnauthorizedAccessException("Access denied");

      var configurationsToUpdate = _mapper.Map<DynamicConfigurationsModel[]>(req);
      return new JsonResult(await _configurationService.UpdateDynamicConfigurationsAsync(configurationsToUpdate));
    }
    catch (UnauthorizedAccessException e)
    {
      return new ObjectResult(e.Message)
      {
        StatusCode = StatusCodes.Status401Unauthorized
      };
    }

  }
}
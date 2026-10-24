using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using WebAppFunction.Api.Entities;
using WebAppFunction.Api.Models;
using WebAppFunction.Api.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppFunction.Api.Functions;

public class Administration
{
  private readonly IAdministrationService _administrationService;
  private readonly IMapper _mapper;

  public Administration(
      IAdministrationService administrationService,
      IMapper mapper
  )
  {
    _administrationService = administrationService;
    _mapper = mapper;
  }

  #region Grantor Type

  [FunctionName("GetGrantorTypes")]
  public async Task<IActionResult> GetGrantorTypes(
      [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GrantorTypes")]
        HttpRequest req,
      ILogger log
  )
  {
    try
    {
      return new JsonResult(await _administrationService.GetGrantorTypeAsync());
    }
    catch (Exception ex)
    {
      return HandleException(nameof(this.GetGrantorTypes), ex, log);
    }
  }

  [FunctionName("PostGrantorType")]
  public async Task<IActionResult> PostGrantorType(
      [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "GrantorType")]
        HttpRequest req,
      ILogger log
  )
  {
    try
    {
      CheckAuth(req);

      var grantorType = _mapper.Map<GrantorType>(req);
      return new JsonResult(await _administrationService.UpdateGrantorTypeAsync(grantorType));
    }
    catch (Exception ex)
    {
      return HandleException(nameof(this.PostGrantorType), ex, log);
    }
  }

  #endregion


  private void CheckAuth(HttpRequest req)
  {
    AuthModel authModel = _mapper.Map<AuthModel>(req);
    if (!authModel.Roles.Contains(AuthRoles.Administrator))
      throw new UnauthorizedAccessException("Access denied");
  }

  private IActionResult HandleException(string funcName, Exception ex, ILogger log)
  {
    if (ex is UnauthorizedAccessException)
      return new ObjectResult(ex.Message) { StatusCode = StatusCodes.Status401Unauthorized };

    log.LogError(ex, $"Error in {funcName}");
    return new ObjectResult(ex.Message) { StatusCode = StatusCodes.Status500InternalServerError };
  }
}
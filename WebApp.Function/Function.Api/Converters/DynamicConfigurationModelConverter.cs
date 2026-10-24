using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using WebAppFunction.Api.Models;

namespace WebAppFunction.Api.Converters;

public class DynamicConfigurationModelConverter : ITypeConverter<HttpRequest, DynamicConfigurationsModel[]>
{
  private static T Convert<T>(HttpRequest request) =>
      JsonConvert.DeserializeObject<T>(request.ReadAsStringAsync().Result);

  public DynamicConfigurationsModel[] Convert(HttpRequest source, DynamicConfigurationsModel[] configModels, ResolutionContext context)
  {
    configModels = Convert<DynamicConfigurationsModel[]>(source);

    return configModels;
  }
}
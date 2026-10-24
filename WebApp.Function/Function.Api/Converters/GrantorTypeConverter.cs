using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using WebAppFunction.Api.Entities;

namespace WebAppFunction.Api.Converters;

public class GrantorTypeConverter : ITypeConverter<HttpRequest, GrantorType>
{
  private static T Convert<T>(HttpRequest request) =>
      JsonConvert.DeserializeObject<T>(request.ReadAsStringAsync().Result);

  public GrantorType Convert(HttpRequest source, GrantorType grantorType, ResolutionContext context)
  {
    grantorType = Convert<GrantorType>(source);

    return grantorType;
  }
}
using AutoMapper;
using WebAppFunction.Api.Converters;
using WebAppFunction.Api.Entities;
using WebAppFunction.Api.Models;
using Microsoft.AspNetCore.Http;

namespace WebAppFunction.Api.Profiles;

public class DynamicConfigurationProfile : Profile
{
  public DynamicConfigurationProfile()
  {
    CreateMap<DynamicConfigurations, DynamicConfigurationsModel>();

    CreateMap<HttpRequest, DynamicConfigurationsModel[]>()
        .ConvertUsing<DynamicConfigurationModelConverter>();
  }
}
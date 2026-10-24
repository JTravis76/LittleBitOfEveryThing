using AutoMapper;
using WebAppFunction.Api.Converters;
using WebAppFunction.Api.Entities;
using Microsoft.AspNetCore.Http;

namespace WebAppFunction.Api.Profiles;

public class GrantorTypeProfile : Profile
{
  public GrantorTypeProfile()
  {
    CreateMap<HttpRequest, GrantorType>()
        .ConvertUsing<GenericRequestConverter<GrantorType>>();
  }
}
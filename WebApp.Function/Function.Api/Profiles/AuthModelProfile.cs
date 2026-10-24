using AutoMapper;
using WebAppFunction.Api.Converters;
using WebAppFunction.Api.Models;
using Microsoft.AspNetCore.Http;

namespace WebAppFunction.Api.Profiles;

public class AuthModelProfile : Profile
{
  public AuthModelProfile()
  {
    CreateMap<HttpRequest, AuthModel>()
        .ConvertUsing<AuthModelConverter>();
  }
}
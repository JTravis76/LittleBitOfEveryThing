using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using WebAppFunction.Api.Extensions;
using WebAppFunction.Api.Models;

namespace WebAppFunction.Api.Converters;

public class AuthModelConverter : ITypeConverter<HttpRequest, AuthModel>
{
  public AuthModel Convert(HttpRequest source, AuthModel destination, ResolutionContext context)
  {
    // get all Authorization headers from request
    if (!source.Headers.TryGetValue(HeaderNames.Authorization, out var authHeaders))
      throw new UnauthorizedAccessException("No token in request");

    var handler = new JwtSecurityTokenHandler();

    // Strip "Bearer " from the header, leaving only the token value
    var tokenString = authHeaders[0].Remove(0, "Bearer ".Length);

    try
    {
      return Convert(handler.ReadToken(tokenString) as JwtSecurityToken);
    }
    catch (Exception e)
    {
      throw new UnauthorizedAccessException($"Invalid {HeaderNames.Authorization} header", e);
    }
  }

  private static AuthModel Convert(JwtSecurityToken token) =>
      new()
      {
        AzureUserId = token.GetAzureUserId(),
        DisplayName = token.GetDisplayName(),
        PreferredUsername = token.GetPreferredUsername(),
        Roles = token.Payload.Any(x => x.Key == "roles")
              ? (string[])JsonExtensions.Deserializer(token.GetUserRoles(), typeof(string[]))
              : Array.Empty<string>()
      };
}
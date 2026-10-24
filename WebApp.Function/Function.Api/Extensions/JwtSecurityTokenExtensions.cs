using System.IdentityModel.Tokens.Jwt;
using WebAppFunction.Api.Models;

namespace WebAppFunction.Api.Extensions;

public static class JwtSecurityTokenExtensions
{
  public static string GetClaimString(this JwtSecurityToken token, string key) =>
      token.Payload[key].ToString();

  public static string GetAzureUserId(this JwtSecurityToken token) =>
      token.GetClaimString(AuthModel.AzureUserIdKey);

  public static string GetDisplayName(this JwtSecurityToken token) =>
      token.GetClaimString(AuthModel.DisplayNameKey);

  public static string GetPreferredUsername(this JwtSecurityToken token) =>
      token.GetClaimString(AuthModel.PreferredUsernameKey);

  public static string GetUserRoles(this JwtSecurityToken token) =>
      token.GetClaimString(AuthModel.UserRolesKey);
}
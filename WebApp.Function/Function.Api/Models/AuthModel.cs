namespace WebAppFunction.Api.Models;

/// <summary>
/// https://docs.microsoft.com/en-us/azure/active-directory/develop/access-tokens
/// </summary>
public class AuthModel
{
  public const string DisplayNameKey = "name";
  public const string PreferredUsernameKey = "preferred_username";
  public const string AzureUserIdKey = "oid";
  public const string UserRolesKey = "roles";

  /// <summary>
  /// <see cref="AzureUserIdKey"/>
  ///
  /// The immutable identifier for the "principal" of the request - the user or service principal
  /// whose identity has been verified. In ID tokens and app+user tokens, this is the object ID
  /// of the user. In app-only tokens, this is the object ID of the calling service principal.
  /// It can also be used to perform authorization checks safely and as a key in database tables.
  /// This ID uniquely identifies the principal across applications - two different applications
  /// signing in the same user will receive the same value in the oid claim. Thus, oid can be
  /// used when making queries to Microsoft online services, such as the Microsoft Graph. The
  /// Microsoft Graph will return this ID as the id property for a given user account. Because
  /// the oid allows multiple apps to correlate principals, the profile scope is required in
  /// order to receive this claim for users. If a single user exists in multiple tenants, the
  /// user will contain a different object ID in each tenant - they are considered different accounts,
  /// even though the user logs into each account with the same credentials.
  /// </summary>
  public string AzureUserId { get; set; }

  /// <summary>
  /// <see cref="PreferredUsernameKey"/>
  ///
  /// The primary username that represents the user. It could be an email address,
  /// phone number, or a generic username without a specified format. Its value is mutable
  /// nd might change over time. Since it is mutable, this value must not be used to make
  /// authorization decisions. It can be used for username hints, however, and in human-readable
  /// UI as a username. The profile scope is required in order to receive this claim.
  /// Present only in v2.0 tokens.
  /// </summary>
  public string PreferredUsername { get; set; }

  /// <summary>
  /// <see cref="DisplayNameKey"/>
  ///
  /// Provides a human-readable value that identifies the subject of the token. The value is not
  /// guaranteed to be unique, it is mutable, and it's designed to be used only for display purposes.
  /// The profile scope is required in order to receive this claim.
  /// </summary>
  public string DisplayName { get; set; }

  /// <summary>
  /// <see cref="UserRolesKey"/>
  ///
  /// Provides a list a roles that defines the user's permissions
  /// </summary>
  public string[] Roles { get; set; }
}

public static class AuthRoles
{
  public const string Supervisor = "Supervisor";
  public const string Administrator = "Administrator";
}
namespace Aidelythe.Application._System.Authentication.Projections;

/// <summary>
/// Represents information about an issued access token.
/// </summary>
/// <param name="Token">The string representation of the access token.</param>
/// <param name="ExpiresIn">The duration in seconds until the token expires.</param>
public readonly record struct AccessTokenInfo(string Token, int ExpiresIn);
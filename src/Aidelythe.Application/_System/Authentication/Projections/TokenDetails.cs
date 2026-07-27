namespace Aidelythe.Application._System.Authentication.Projections;

/// <summary>
/// Represents the details of a token.
/// </summary>
/// <param name="Token">The string representation of the token.</param>
/// <param name="ExpiresAt">The date and time when the token expires.</param>
public readonly record struct TokenDetails(string Token, DateTimeOffset ExpiresAt);
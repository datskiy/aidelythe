namespace Aidelythe.Application._System.Authentication.Projections;

/// <summary>
/// Represents information about an issued token.
/// </summary>
/// <param name="Token">The string representation of the token.</param>
/// <param name="ExpiresIn">The duration in seconds until the token expires.</param>
public readonly record struct TokenInfo(string Token, int ExpiresIn);
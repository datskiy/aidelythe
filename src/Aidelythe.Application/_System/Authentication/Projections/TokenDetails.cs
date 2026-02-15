namespace Aidelythe.Application._System.Authentication.Projections;

/// <summary>
/// Represents the details of a token.
/// </summary>
/// <param name="Token">The string representation of the token.</param>
/// <param name="ExpiresIn">The expiration time of the token in seconds.</param>
public readonly record struct TokenDetails(string Token, int ExpiresIn);
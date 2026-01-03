namespace Aidelythe.Application._System.Authentication.Projections;

/// <summary>
/// Represents a pair of access and refresh tokens.
/// </summary>
/// <param name="AccessToken">Information about the access token.</param>
/// <param name="RefreshToken">Information about the refresh token.</param>
public readonly record struct TokenPair(TokenInfo AccessToken, TokenInfo RefreshToken);
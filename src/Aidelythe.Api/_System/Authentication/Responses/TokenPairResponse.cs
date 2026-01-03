namespace Aidelythe.Api._System.Authentication.Responses;

/// <summary>
/// Represents a response for a token pair.
/// </summary>
public sealed class TokenPairResponse
{
    /// <summary>
    /// Gets information about the access token.
    /// </summary>
    [JsonPropertyName("accessToken")]
    public required TokenInfoResponse AccessToken { get; init; }

    /// <summary>
    /// Gets information about the refresh token.
    /// </summary>
    [JsonPropertyName("refreshToken")]
    public required TokenInfoResponse RefreshToken { get; init; }
}
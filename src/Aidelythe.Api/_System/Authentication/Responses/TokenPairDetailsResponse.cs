namespace Aidelythe.Api._System.Authentication.Responses;

/// <summary>
/// Represents a response for the details of a token pair.
/// </summary>
public sealed class TokenPairDetailsResponse
{
    /// <summary>
    /// Gets the refresh token.
    /// </summary>
    [JsonPropertyName("refreshToken")]
    public required TokenDetailsResponse RefreshToken { get; init; }

    /// <summary>
    /// Gets the access token.
    /// </summary>
    [JsonPropertyName("accessToken")]
    public required TokenDetailsResponse AccessToken { get; init; }
}
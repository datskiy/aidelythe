namespace Aidelythe.Api._System.Authentication.Responses;

/// <summary>
/// Represents a response for the details of a token.
/// </summary>
public sealed class TokenDetailsResponse
{
    /// <summary>
    /// Gets the string representation of the token.
    /// </summary>
    [JsonPropertyName("token")]
    public required string Token { get; init; }

    /// <summary>
    /// Gets the date and time when the token expires.
    /// </summary>
    [JsonPropertyName("expiresAt")]
    public DateTimeOffset ExpiresAt { get; init; }
}
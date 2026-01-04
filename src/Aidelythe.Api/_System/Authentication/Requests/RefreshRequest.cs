namespace Aidelythe.Api._System.Authentication.Requests;

/// <summary>
/// Represents a request to refresh the token pair of a user.
/// </summary>
public sealed class RefreshRequest
{
    /// <summary>
    /// Gets the refresh token of the user.
    /// </summary>
    [JsonPropertyName("refreshToken")]
    [Required]
    public string? RefreshToken { get; init; }
}
using EmailType = Aidelythe.Application._System.Authentication.ValueObjects.Email;
using PasswordType = Aidelythe.Application._System.Authentication.ValueObjects.Password;

namespace Aidelythe.Api._System.Authentication.Requests;

/// <summary>
/// Represents a request to log in a user.
/// </summary>
public sealed class LoginRequest
{
    /// <summary>
    /// Gets the login of the user.
    /// </summary>
    [JsonPropertyName("login")]
    [Required]
    [MaxLength(EmailType.MaximumLength)]
    public string? Login { get; init; }

    /// <summary>
    /// Gets the password of the user.
    /// </summary>
    [JsonPropertyName("password")]
    [Required]
    [Length(PasswordType.MinimumLength, PasswordType.MaximumLength)]
    public string? Password { get; init; }
}
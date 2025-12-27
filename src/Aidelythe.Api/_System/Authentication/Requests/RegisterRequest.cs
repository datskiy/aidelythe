using EmailType = Aidelythe.Application._System.Authentication.ValueObjects.Email;
using PhoneNumberType = Aidelythe.Application._System.Authentication.ValueObjects.PhoneNumber;
using PasswordType = Aidelythe.Application._System.Authentication.ValueObjects.Password;

namespace Aidelythe.Api._System.Authentication.Requests;

/// <summary>
/// Represents a request to register a user.
/// </summary>
public sealed class RegisterRequest
{
    /// <summary>
    /// Gets the email address of the user.
    /// </summary>
    [JsonPropertyName("email")]
    [MaxLength(EmailType.MaximumLength)]
    [RegularExpression(EmailType.FormatPattern)]
    public string? Email { get; init; }

    /// <summary>
    /// Gets the phone number of the user.
    /// </summary>
    [JsonPropertyName("phoneNumber")]
    [RegularExpression(PhoneNumberType.FormatPattern)]
    public string? PhoneNumber { get; init; }

    /// <summary>
    /// Gets the password for the user.
    /// </summary>
    [JsonPropertyName("password")]
    [Required]
    [Length(PasswordType.MinimumLength, PasswordType.MaximumLength)]
    public string? Password { get; init; }
}
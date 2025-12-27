using Aidelythe.Application._System.Authentication.Results;

namespace Aidelythe.Application._System.Authentication.Commands;

/// <summary>
/// Represents a command to register a user.
/// </summary>
public sealed class RegisterCommand : IRequest<RegisterResult>
{
    /// <summary>
    /// Gets the email address of the user.
    /// </summary>
    public string? Email { get; }

    /// <summary>
    /// Gets the phone number of the user.
    /// </summary>
    public string? PhoneNumber { get; }

    /// <summary>
    /// Gets the password of the user.
    /// </summary>
    public string Password { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RegisterCommand"/> class.
    /// </summary>
    /// <param name="email">The email address of the user.</param>
    /// <param name="phoneNumber">The phone number of the user.</param>
    /// <param name="password">The password of the user.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="password"/> is null.</exception>
    public RegisterCommand(
        string? email,
        string? phoneNumber,
        string password)
    {
        ThrowIfNull(password);

        Email = email;
        PhoneNumber = phoneNumber;
        Password = password;
    }
}
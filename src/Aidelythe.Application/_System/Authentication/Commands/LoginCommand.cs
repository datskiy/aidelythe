using Aidelythe.Application._System.Authentication.Results;

namespace Aidelythe.Application._System.Authentication.Commands;

/// <summary>
/// Represents a command to log in a user.
/// </summary>
public sealed class LoginCommand : IRequest<LoginResult>
{
    /// <summary>
    /// Gets the login of the user.
    /// </summary>
    public string Login { get; }

    /// <summary>
    /// Gets the password of the user.
    /// </summary>
    public string Password { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="LoginCommand"/> class.
    /// </summary>
    /// <param name="login">The login of the user.</param>
    /// <param name="password">The password of the user.</param>
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="login"/> or <paramref name="password"/> is null.
    /// </exception>
    public LoginCommand(
        string login,
        string password)
    {
        ThrowIfNull(login);
        ThrowIfNull(password);

        Login = login;
        Password = password;
    }
}
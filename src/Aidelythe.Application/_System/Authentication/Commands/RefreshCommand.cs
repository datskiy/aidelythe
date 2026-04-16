using Aidelythe.Application._System.Authentication.Results;

namespace Aidelythe.Application._System.Authentication.Commands;

/// <summary>
/// Represents a command to refresh the token pair of a user.
/// </summary>
public sealed class RefreshCommand : IRequest<RefreshResult>
{
    /// <summary>
    /// Gets the refresh token of the user.
    /// </summary>
    public string RefreshToken { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RefreshCommand"/> class.
    /// </summary>
    /// <param name="refreshToken">The refresh token of the user.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="refreshToken"/> is null.</exception>
    public RefreshCommand(string refreshToken)
    {
        ThrowIfNull(refreshToken);

        RefreshToken = refreshToken;
    }
}
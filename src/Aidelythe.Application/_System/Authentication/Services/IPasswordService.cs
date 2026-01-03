using Aidelythe.Application._Common.Discriminants;
using Aidelythe.Application._System.Authentication.Discriminants;
using Aidelythe.Application._System.Authentication.ValueObjects;

namespace Aidelythe.Application._System.Authentication.Services;

/// <summary>
/// Represents a service for managing passwords.
/// </summary>
public interface IPasswordService
{
    /// <summary>
    /// Hashes the specified password.
    /// </summary>
    /// <param name="password">The password to hash.</param>
    /// <returns>
    /// A hashed password.
    /// </returns>
    /// <exception cref="ArgumentNullException">The <paramref name="password"/> is null.</exception>
    PasswordHash Hash(Password password);

    /// <summary>
    /// Verifies the specified password against the specified hashed password.
    /// </summary>
    /// <param name="password">The password to verify.</param>
    /// <param name="hash">The hashed password to verify against.</param>
    /// <returns>
    /// The result of the password verification.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when either <paramref name="password"/> or <paramref name="hash"/> is null.
    /// </exception>
    OneOf<Success, SuccessRehashNeeded, Failure> Verify(
        Password password,
        PasswordHash hash);
}
using Aidelythe.Application._Common.Discriminants;
using Aidelythe.Application._System.Authentication.Discriminants;
using Aidelythe.Application._System.Authentication.Services;
using Aidelythe.Application._System.Authentication.ValueObjects;
using Aidelythe.Domain.Identity.Users;

namespace Aidelythe.Infrastructure._System.Authentication.Services;

/// <summary>
/// Represents a service for managing passwords.
/// </summary>
public sealed class PasswordService : IPasswordService
{
    private readonly PasswordHasher<User> _passwordHasher;

    /// <summary>
    /// Initializes a new instance of the <see cref="PasswordService"/> class.
    /// </summary>
    public PasswordService()
    {
        _passwordHasher = new PasswordHasher<User>();
    }

    /// <inheritdoc/>
    public PasswordHash Hash(Password password)
    {
        ThrowIfNull(password);

        var hash = _passwordHasher.HashPassword(user: null!, password.Value);
        return new PasswordHash(hash);
    }

    /// <inheritdoc/>
    public OneOf<Success, SuccessRehashNeeded, Failure> Verify(
        Password password,
        PasswordHash hash)
    {
        ThrowIfNull(password);
        ThrowIfNull(hash);

        var verificationResult = _passwordHasher.VerifyHashedPassword(
            user: null!,
            hash.Value,
            password.Value);

        return Map(verificationResult);
    }

    private static OneOf<Success, SuccessRehashNeeded, Failure> Map(PasswordVerificationResult verificationResult)
    {
        return verificationResult switch
        {
            PasswordVerificationResult.Success => new Success(),
            PasswordVerificationResult.SuccessRehashNeeded => new SuccessRehashNeeded(),
            PasswordVerificationResult.Failed => new Failure(),
            _ => throw new ArgumentOutOfRangeException(nameof(verificationResult), verificationResult, message: null)
        };
    }
}
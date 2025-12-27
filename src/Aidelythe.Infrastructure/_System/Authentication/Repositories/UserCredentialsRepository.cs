using Aidelythe.Application._System.Authentication.Data;
using Aidelythe.Application._System.Authentication.Repositories;
using Aidelythe.Application._System.Authentication.ValueObjects;

namespace Aidelythe.Infrastructure._System.Authentication.Repositories;

/// <summary>
/// Represents a repository for user credentials.
/// </summary>
public sealed class UserCredentialsRepository : IUserCredentialsRepository
{
    /// <inheritdoc/>
    public Task<bool> ExistsByEmailOrPhoneNumberAsync(
        Email? email,
        PhoneNumber? phoneNumber,
        CancellationToken cancellationToken)
    {
        if (email is null && phoneNumber is null)
            throw new ArgumentException("At least one checking criteria must be provided.");

        // TODO: implement

        return Task.FromResult(false);
    }

    /// <inheritdoc/>
    public Task AddAsync(
        UserCredentials userCredentials,
        CancellationToken cancellationToken)
    {
        ThrowIfNull(userCredentials);

        // TODO: implement

        return Task.CompletedTask;
    }
}
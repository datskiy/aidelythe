using Aidelythe.Application._System.Authentication.Data;
using Aidelythe.Application._System.Authentication.Repositories;
using Aidelythe.Application._System.Authentication.ValueObjects;
using Aidelythe.Domain.Identity.Users.ValueObjects;

namespace Aidelythe.Infrastructure._System.Authentication.Repositories;

/// <summary>
/// Represents a repository for user credentials.
/// </summary>
public sealed class UserCredentialsRepository : IUserCredentialsRepository
{
    /// <inheritdoc/>
    public Task<bool> ExistsAsync(
        Email email,
        CancellationToken cancellationToken)
    {
        ThrowIfNull(email);

        // TODO: implement

        return Task.FromResult(false);
    }

    /// <inheritdoc/>
    public Task<bool> ExistsAsync(
        PhoneNumber phoneNumber,
        CancellationToken cancellationToken)
    {
        ThrowIfNull(phoneNumber);

        // TODO: implement

        return Task.FromResult(false);
    }

    /// <inheritdoc/>
    public Task<UserCredentials?> GetAsync(
        Email email,
        CancellationToken cancellationToken)
    {
        ThrowIfNull(email);

        // TODO: implement

        return Task.FromResult(new UserCredentials(
            UserId.New(),
            email,
            phoneNumber: null,
            new PasswordHash("hashed-password")))!;
    }

    /// <inheritdoc/>
    public Task<UserCredentials?> GetAsync(
        PhoneNumber phoneNumber,
        CancellationToken cancellationToken)
    {
        ThrowIfNull(phoneNumber);

        // TODO: implement

        return Task.FromResult(new UserCredentials(
            UserId.New(),
            email: null,
            phoneNumber,
            new PasswordHash("hashed-password")))!;
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

    /// <inheritdoc/>
    public Task UpdateAsync(
        UserCredentials userCredentials,
        CancellationToken cancellationToken)
    {
        ThrowIfNull(userCredentials);

        // TODO: implement

        return Task.CompletedTask;
    }
}
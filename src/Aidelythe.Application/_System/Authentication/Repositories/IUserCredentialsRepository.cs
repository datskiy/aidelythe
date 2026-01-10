using Aidelythe.Application._System.Authentication.Data;
using Aidelythe.Application._System.Authentication.ValueObjects;

namespace Aidelythe.Application._System.Authentication.Repositories;

/// <summary>
/// Represents a repository for user credentials.
/// </summary>
public interface IUserCredentialsRepository // TODO: use GenericRepository
{
    /// <summary>
    /// Determines whether a user exists with the specified email or phone number.
    /// </summary>
    /// <param name="email">The email address to check.</param>
    /// <param name="phoneNumber">The phone number to check.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains a value indicating whether a user exists with the specified email or phone number.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Both <paramref name="email"/> and <paramref name="phoneNumber"/> are null.
    /// </exception>
    Task<bool> ExistsByEmailOrPhoneNumberAsync(
        Email? email,
        PhoneNumber? phoneNumber,
        CancellationToken cancellationToken);

    /// <summary>
    /// Gets the user credentials associated with the specified email address.
    /// </summary>
    /// <param name="email">The email address to look up.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the user credentials token associated with the email address,
    /// or null if no such user credentials exist.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="email"/> is null.</exception>
    Task<UserCredentials?> GetAsync(
        Email email,
        CancellationToken cancellationToken);

    /// <summary>
    /// Gets the user credentials associated with the specified phone number.
    /// </summary>
    /// <param name="phoneNumber">The phone number to look up.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the user credentials token associated with the phone number,
    /// or null if no such user credentials exist.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="phoneNumber"/> is null.</exception>
    Task<UserCredentials?> GetAsync(
        PhoneNumber phoneNumber,
        CancellationToken cancellationToken);

    /// <summary>
    /// Adds the specified user credentials.
    /// </summary>
    /// <param name="userCredentials">The user credentials to add.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="userCredentials"/> is null.</exception>
    Task AddAsync(
        UserCredentials userCredentials,
        CancellationToken cancellationToken);

    /// <summary>
    /// Updates the specified user credentials.
    /// </summary>
    /// <param name="userCredentials">The user credentials to update.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="userCredentials"/> is null.</exception>
    Task UpdateAsync(
        UserCredentials userCredentials,
        CancellationToken cancellationToken);

}

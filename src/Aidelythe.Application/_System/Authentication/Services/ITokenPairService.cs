using Aidelythe.Application._System.Authentication.Projections;
using Aidelythe.Domain.Identity.Users.ValueObjects;

namespace Aidelythe.Application._System.Authentication.Services;

/// <summary>
/// Represents a service for managing token pairs.
/// </summary>
public interface ITokenPairService
{
    /// <summary>
    /// Generates a token pair for the specified user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user for whom the refresh token is being issued.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains information about the generated token pair.
    /// </returns>
    Task<TokenPair> GenerateAsync(
        UserId userId,
        CancellationToken cancellationToken);
}
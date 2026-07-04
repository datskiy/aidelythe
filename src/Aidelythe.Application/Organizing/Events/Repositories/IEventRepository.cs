using Aidelythe.Domain.Identity.Users;

namespace Aidelythe.Application.Organizing.Events.Repositories;

/// <summary>
/// Represents a repository for events.
/// </summary>
public interface IEventRepository // TODO: use GenericRepository
{
    /// <summary>
    /// Purges all events deleted before the specified cutoff date.
    /// </summary>
    /// <param name="cutoff">The date before which deleted events will be purged.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the number of deleted events purged.
    /// </returns>
    /// <exception cref="ArgumentException">The <paramref name="cutoff"/> is not in UTC.</exception>
    Task<int> PurgeDeletedOlderThanAsync(
        DateTime cutoff,
        CancellationToken cancellationToken);
}

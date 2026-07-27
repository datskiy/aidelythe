using Aidelythe.Application.Organizing.Events.Repositories;

namespace Aidelythe.Infrastructure.Organizing.Events.Repositories;

/// <summary>
/// Represents a repository for users.
/// </summary>
public sealed class EventRepository : IEventRepository
{
    /// <inheritdoc/>
    public Task<int> PurgeDeletedOlderThanAsync(DateOnly cutoff, CancellationToken cancellationToken)
    {
        // TODO: implement

        return Task.FromResult(67);
    }
}
using Aidelythe.Application._Common.Persistence;

namespace Aidelythe.Infrastructure._System.Persistence;

/// <summary>
/// Represents a unit of work that ensures consistency when performing changes to the database.
/// </summary>
public sealed class UnitOfWork : IUnitOfWork
{
    /// <inheritdoc/>
    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        // TODO: implement

        return Task.CompletedTask;
    }
}
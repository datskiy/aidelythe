namespace Aidelythe.Application._Common.Persistence;

/// <summary>
/// Represents a unit of work that ensures consistency when performing changes to the database.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Saves all changes made in the current context to the database.
    /// </summary>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// </returns>
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
using Aidelythe.Api.Organizing.Events.Resources;
using Aidelythe.Application._Common.Discriminants;
using Aidelythe.Shared.DiscriminatedUnion;

namespace Aidelythe.Api.Organizing.Events.Mappers;

/// <summary>
/// Provides mapping methods for event-related discriminants.
/// </summary>
public static class EventProblemDetailsMapper
{
    /// <summary>
    /// Maps the <see cref="IDiscriminant"/> instance to its corresponding problem details string representation.
    /// </summary>
    /// <typeparam name="TDiscriminant">
    /// The type of the discriminant implementing the <see cref="IDiscriminant"/> interface.
    /// </typeparam>
    /// <param name="discriminant">The <see cref="IDiscriminant"/> to map.</param>
    /// <returns>
    /// A string representation of the problem details associated with the given discriminant.
    /// </returns>
    /// <exception cref="ArgumentNullException">The <paramref name="discriminant"/> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// The <paramref name="discriminant"/> does not match a recognized type.
    /// </exception>
    public static string ToProblemDetails<TDiscriminant>(this TDiscriminant discriminant)
        where TDiscriminant : IDiscriminant
    {
        ThrowIfNull(discriminant);

        return discriminant switch
        {
            AlreadyExists => EventProblemDetails.AlreadyExists,
            InvalidDateRange => EventProblemDetails.InvalidDateRange,
            _ => throw new ArgumentOutOfRangeException(nameof(discriminant), discriminant, message: null)
        };
    }
}
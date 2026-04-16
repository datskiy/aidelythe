using Aidelythe.Shared.ValueObjects;

namespace Aidelythe.Domain.Organizing.Events.ValueObjects;

/// <summary>
/// Represents the description of an event.
/// </summary>
public sealed record EventDescription : PlainValueString
{
    /// <summary>
    /// The maximum acceptable length of the description.
    /// </summary>
    public const int MaximumLength = 500;

    /// <summary>
    /// Initializes a new instance of the <see cref="EventDescription"/> class.
    /// </summary>
    /// <param name="value">The description of the event.</param>
    /// <exception cref="ArgumentException">
    /// The <paramref name="value"/> is null, empty, or consists only of white-space characters.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// The <paramref name="value"/> is longer than <see cref="MaximumLength"/>.
    /// </exception>
    public EventDescription(string value) : base(
        value,
        minimumLength: null,
        MaximumLength)
    {
    }
}
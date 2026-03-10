using Aidelythe.Shared.ValueObjects;

namespace Aidelythe.Domain.Organizing.Events.ValueObjects;

/// <summary>
/// Represents the title of an event.
/// </summary>
public sealed record EventTitle : PlainValueString
{
    /// <summary>
    /// The maximum acceptable length of the title.
    /// </summary>
    public const int MaximumLength = 100;

    /// <summary>
    /// Initializes a new instance of the <see cref="EventTitle"/> class.
    /// </summary>
    /// <param name="value">The title of the event.</param>
    /// <exception cref="ArgumentException">
    /// The <paramref name="value"/> is null, empty, or consists only of white-space characters.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// The <paramref name="value"/> is longer than <see cref="MaximumLength"/>.
    /// </exception>
    public EventTitle(string value) : base(
        value,
        minimumLength: null,
        MaximumLength)
    {
    }
}
using Aidelythe.Domain._Common.Locality;

namespace Aidelythe.Domain.Organizing.Events.ValueObjects;

/// <summary>
/// Represents an address of an event.
/// </summary>
public sealed record EventAddress : Address
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EventAddress"/> class.
    /// </summary>
    /// <inheritdoc/>
    public EventAddress(
        string country,
        string? region = null,
        string? city = null,
        string? postalCode = null,
        string? street = null) : base(
            country,
            region,
            city,
            postalCode,
            street)
    {
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{Country}, {Region}, {City}, {PostalCode}, {Street}";
    }
}
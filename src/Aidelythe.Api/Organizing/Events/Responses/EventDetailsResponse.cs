using Aidelythe.Api._Common.Locality.Responses;

namespace Aidelythe.Api.Organizing.Events.Responses;

/// <summary>
/// Represents details of an event.
/// </summary>
public sealed class EventDetailsResponse // TODO: add event owner
{
    /// <summary>
    /// Gets the unique identifier of the event.
    /// </summary>
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }

    /// <summary>
    /// Gets the title of the event.
    /// </summary>
    [JsonPropertyName("title")]
    public required string Title { get; init; }

    /// <summary>
    /// Gets the description of the event.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>
    /// Gets the location of the event.
    /// </summary>
    [JsonPropertyName("location")]
    public required AddressDetailsResponse Location { get; init; }

    /// <summary>
    /// Gets the date when the event starts.
    /// </summary>
    [JsonPropertyName("startsAt")]
    public DateTime StartsAt { get; init; }

    /// <summary>
    /// Gets the date when the event ends.
    /// </summary>
    [JsonPropertyName("endsAt")]
    public DateTime? EndsAt { get; init; }

    /// <summary>
    /// Gets the date when the event was created.
    /// </summary>
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; init; }

    /// <summary>
    /// Gets the date when the event was last updated.
    /// </summary>
    [JsonPropertyName("lastUpdatedAt")]
    public DateTime? LastUpdatedAt { get; init; }
}
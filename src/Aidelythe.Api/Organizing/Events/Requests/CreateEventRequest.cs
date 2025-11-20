using Aidelythe.Api._Common.Locality;

namespace Aidelythe.Api.Organizing.Events.Requests;

/// <summary>
/// A request to create an event.
/// </summary>
public sealed class CreateEventRequest
{
    /// <summary>
    /// The title of an event.
    /// </summary>
    [Required]
    [JsonPropertyName("title")]
    public string? Title { get; init; }

    /// <summary>
    /// The description of an event.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>
    /// The location of the event.
    /// </summary>
    [Required]
    [JsonPropertyName("location")]
    public AddressRequest? Location { get; init; }

    /// <summary>
    /// The date when the event starts.
    /// </summary>
    [Required]
    [JsonPropertyName("startsAt")]
    public DateTime? StartsAt { get; init; }

    /// <summary>
    /// The date when the event ends.
    /// </summary>
    [JsonPropertyName("endsAt")]
    public DateTime? EndsAt { get; init; }
}
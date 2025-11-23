using Aidelythe.Api._Common.Locality;
using Aidelythe.Domain.Organizing.Events.ValueObjects;

namespace Aidelythe.Api.Organizing.Events.Requests;

/// <summary>
/// Represents a request to update an event.
/// </summary>
public sealed class UpdateEventRequest
{
    /// <summary>
    /// Gets the title of the event.
    /// </summary>
    [JsonPropertyName("title")]
    [Required]
    [MaxLength(EventTitle.MaximumLength)]
    public string? Title { get; init; }

    /// <summary>
    /// Gets the description of the event.
    /// </summary>
    [JsonPropertyName("description")]
    [MaxLength(EventDescription.MaximumLength)]
    public string? Description { get; init; }

    /// <summary>
    /// Gets the location of the event.
    /// </summary>
    [JsonPropertyName("location")]
    [Required]
    public AddressRequest? Location { get; init; }

    /// <summary>
    /// Gets the date when the event starts.
    /// </summary>
    [JsonPropertyName("startsAt")]
    [Required]
    public DateTime? StartsAt { get; init; }

    /// <summary>
    /// Gets the date when the event ends.
    /// </summary>
    [JsonPropertyName("endsAt")]
    public DateTime? EndsAt { get; init; }
}
namespace Aidelythe.Api.Organizing.Responses;

/// <summary>
/// Details of an event.
/// </summary>
public sealed class EventDetailsResponse
{
    /// <summary>
    /// The unique identifier of the event.
    /// </summary>
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }

    /// <summary>
    /// The title of the event.
    /// </summary>
    [JsonPropertyName("title")]
    public required string Title { get; init; }

    /// <summary>
    /// The description of the event.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }
}
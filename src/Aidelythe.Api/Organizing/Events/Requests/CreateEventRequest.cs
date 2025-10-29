namespace Aidelythe.Api.Organizing.Events.Requests;

/// <summary>
/// A request to create an event.
/// </summary>
public sealed class CreateEventRequest
{
    /// <summary>
    /// The title of an event.
    /// </summary>
    [JsonPropertyName("title")]
    public required string Title { get; init; }

    /// <summary>
    /// The description of an event.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }
}
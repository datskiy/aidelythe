namespace Aidelythe.Api.Organizing.Requests;

/// <summary>
/// A request to update an event.
/// </summary>
public sealed class UpdateEventRequest
{
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
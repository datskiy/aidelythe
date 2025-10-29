namespace Aidelythe.Api.Organizing.Responses;

/// <summary>
/// A summary of an event.
/// </summary>
public sealed class EventSummaryResponse
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
}
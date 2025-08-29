namespace Aidelythe.Api.Organizing.Responses;

/// <summary>
/// TODO: desc here + for all props + add validation attributes for Swagger + finish full MVP contract
/// </summary>
public sealed class EventResponse
{
    [JsonProperty("id")]
    public required Guid Id { get; init; }

    [JsonProperty("title")]
    public required string Title { get; init; }

    [JsonProperty("description")]
    public string? Description { get; init; }
}
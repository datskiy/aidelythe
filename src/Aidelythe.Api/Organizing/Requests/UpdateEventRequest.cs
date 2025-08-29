namespace Aidelythe.Api.Organizing.Requests;

/// <summary>
/// TODO: desc here + for all props + add validation attributes for Swagger + finish full MVP contract
/// </summary>
public sealed class UpdateEventRequest
{
    // TODO: consider [JsonProperty("title", Required = Required.Always)]
    [JsonProperty("title")]
    public string? Title { get; init; } // TODO: fix nullability with validation

    [JsonProperty("description")]
    public string? Description { get; init; }
}
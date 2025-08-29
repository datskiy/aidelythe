namespace Aidelythe.Api._Common.Controllers.Http;

/// <summary>
/// TODO: desc
/// </summary>
public readonly struct CreatedResourceResponse
{
    [JsonProperty("id")]
    public Guid Id { get; init; }
}
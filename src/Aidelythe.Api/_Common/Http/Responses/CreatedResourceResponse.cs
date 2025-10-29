namespace Aidelythe.Api._Common.Http.Responses;

/// <summary>
/// Represents a response containing information about a created resource.
/// </summary>
public readonly struct CreatedResourceResponse
{
    /// <summary>
    /// The unique identifier of the created resource.
    /// </summary>
    [JsonPropertyName("id")]
    public Guid Id { get; init; }
}
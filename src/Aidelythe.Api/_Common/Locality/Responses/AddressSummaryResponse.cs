namespace Aidelythe.Api._Common.Locality.Responses;

/// <summary>
/// Represents response for the summary of an address.
/// </summary>
public sealed class AddressSummaryResponse
{
    /// <summary>
    /// Gets the name of the country.
    /// </summary>
    [JsonPropertyName("country")]
    public required string Country { get; init; }

    /// <summary>
    /// Gets the name of the region.
    /// </summary>
    [JsonPropertyName("region")]
    public string? Region { get; init; }

    /// <summary>
    /// Gets the name of the city.
    /// </summary>
    [JsonPropertyName("city")]
    public string? City { get; init; }
}
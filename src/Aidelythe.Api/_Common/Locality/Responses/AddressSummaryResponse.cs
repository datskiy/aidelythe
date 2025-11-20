namespace Aidelythe.Api._Common.Locality.Responses;

/// <summary>
/// Address summary.
/// </summary>
public sealed class AddressSummaryResponse
{
    /// <summary>
    /// The country name of the address.
    /// </summary>
    [JsonPropertyName("country")]
    public required string Country { get; init; }

    /// <summary>
    /// The region name of the address.
    /// </summary>
    [JsonPropertyName("region")]
    public string? Region { get; init; }

    /// <summary>
    /// The city name of the address.
    /// </summary>
    [JsonPropertyName("city")]
    public string? City { get; init; }
}
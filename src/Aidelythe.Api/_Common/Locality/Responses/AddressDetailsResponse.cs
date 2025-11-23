namespace Aidelythe.Api._Common.Locality.Responses;

/// <summary>
/// Represents details of an address.
/// </summary>
public sealed class AddressDetailsResponse
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

    /// <summary>
    /// Gets the name of the street.
    /// </summary>
    [JsonPropertyName("street")]
    public string? Street { get; init; }

    /// <summary>
    /// Gets the postal or ZIP code.
    /// </summary>
    [JsonPropertyName("postalCode")]
    public string? PostalCode { get; init; }
}
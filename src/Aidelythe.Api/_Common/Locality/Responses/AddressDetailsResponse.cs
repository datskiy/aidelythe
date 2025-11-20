namespace Aidelythe.Api._Common.Locality.Responses;

/// <summary>
/// Address details.
/// </summary>
public sealed class AddressDetailsResponse
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

    /// <summary>
    /// The postal or ZIP code of the address.
    /// </summary>
    [JsonPropertyName("postalCode")]
    public string? PostalCode { get; init; }

    /// <summary>
    /// The street name of the address.
    /// </summary>
    [JsonPropertyName("street")]
    public string? Street { get; init; }
}
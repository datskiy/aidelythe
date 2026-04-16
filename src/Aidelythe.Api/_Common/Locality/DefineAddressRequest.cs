using Aidelythe.Domain._Common.Locality;

namespace Aidelythe.Api._Common.Locality;

/// <summary>
/// Represents a request to define an address.
/// </summary>
public sealed class DefineAddressRequest
{
    /// <summary>
    /// Gets the name of the country.
    /// </summary>
    [JsonPropertyName("country")]
    [Required]
    [MaxLength(Address.MaximumAreaNameLength)]
    public string? Country { get; init; }

    /// <summary>
    /// Gets the name of the region.
    /// </summary>
    [JsonPropertyName("region")]
    [MaxLength(Address.MaximumAreaNameLength)]
    public string? Region { get; init; }

    /// <summary>
    /// Gets the name of the city.
    /// </summary>
    [JsonPropertyName("city")]
    [MaxLength(Address.MaximumAreaNameLength)]
    public string? City { get; init; }

    /// <summary>
    /// Gets the name of the street.
    /// </summary>
    [JsonPropertyName("street")]
    [MaxLength(Address.MaximumStreetNameLength)]
    public string? Street { get; init; }

    /// <summary>
    /// Gets the postal or ZIP code.
    /// </summary>
    [JsonPropertyName("postalCode")]
    [MaxLength(Address.MaximumPostalCodeLength)]
    [RegularExpression(Address.PostalCodeFormatPattern)]
    public string? PostalCode { get; init; }
}
namespace Aidelythe.Application._Common.Locality;

/// <summary>
/// Represents the summary of an address.
/// </summary>
public sealed class AddressSummary
{
    /// <summary>
    /// Gets the country name of the address.
    /// </summary>
    public string Country { get; }

    /// <summary>
    /// Gets the region name of the address.
    /// </summary>
    public string? Region { get; }

    /// <summary>
    /// Gets the city name of the address.
    /// </summary>
    public string? City { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AddressSummary"/> class.
    /// </summary>
    /// <param name="country">The country name of the address.</param>
    /// <param name="region">The region name of the address.</param>
    /// <param name="city">The city name of the address.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="country"/> is null.</exception>
    public AddressSummary(
        string country,
        string? region,
        string? city)
    {
        ThrowIfNull(country);

        Country = country;
        Region = region;
        City = city;
    }
}
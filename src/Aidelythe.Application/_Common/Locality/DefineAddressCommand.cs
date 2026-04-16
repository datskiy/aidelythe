namespace Aidelythe.Application._Common.Locality;

/// <summary>
/// Represents a command to define an address.
/// </summary>
public sealed class DefineAddressCommand
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
    /// Gets the postal or ZIP code of the address.
    /// </summary>
    public string? PostalCode { get; }

    /// <summary>
    /// Gets the street name of the address.
    /// </summary>
    public string? Street { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DefineAddressCommand"/> class.
    /// </summary>
    /// <param name="country">The country name of the address.</param>
    /// <param name="region">The region name of the address.</param>
    /// <param name="city">The city name of the address.</param>
    /// <param name="postalCode">The postal or ZIP code of the address.</param>
    /// <param name="street">The street name of the address.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="country"/> is null.</exception>
    public DefineAddressCommand(
        string country,
        string? region,
        string? city,
        string? postalCode,
        string? street)
    {
        ThrowIfNull(country);

        Country = country;
        Region = region;
        City = city;
        PostalCode = postalCode;
        Street = street;
    }
}
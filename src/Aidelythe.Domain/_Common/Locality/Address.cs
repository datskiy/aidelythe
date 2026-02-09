using Aidelythe.Shared.RegularExpressions;

namespace Aidelythe.Domain._Common.Locality;

/// <summary>
/// Represents an address.
/// </summary>
public abstract record Address
{
    /// <summary>
    /// The maximum acceptable length of the administrative area name.
    /// </summary>
    public const int MaximumAreaNameLength = 100;

    /// <summary>
    /// The maximum acceptable length of the postal code.
    /// </summary>
    public const int MaximumPostalCodeLength = 20;

    /// <summary>
    /// The maximum acceptable length of the street name.
    /// </summary>
    public const int MaximumStreetNameLength = 200;

    /// <summary>
    /// A regular expression pattern representing the postal code format.
    /// Ensures the postal code is a non-empty string that contains only letters, digits, spaces, and hyphens.
    /// </summary>
    /// <example>
    /// 13-37
    /// </example>
    public const string PostalCodeFormatPattern = @"^[A-Za-z0-9\-\s]+$";

    /// <summary>
    /// A regular expression representing the postal code format.
    /// Ensures the postal code is a non-empty string that contains only letters, digits, spaces, and hyphens.
    /// </summary>
    /// <example>
    /// 13-37
    /// </example>
    public static readonly Regex PostalCodeFormatRegex = RegexHelper.CreateConfigured(PostalCodeFormatPattern);

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
    /// Initializes a new instance of the <see cref="Address"/> class.
    /// </summary>
    /// <param name="country">The country name of the address.</param>
    /// <param name="region">The region name of the address.</param>
    /// <param name="city">The city name of the address.</param>
    /// <param name="postalCode">The postal or ZIP code of the address.</param>
    /// <param name="street">The street name of the address.</param>
    /// <exception cref="ArgumentException">
    /// The <paramref name="country"/> is null, empty, or consists only of white-space characters.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// The <paramref name="region"/>, <paramref name="city"/>, <paramref name="postalCode"/>
    /// or <paramref name="street"/> is empty, or consists only of white-space characters.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// The <paramref name="postalCode"/> does not match the expected format.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// The <paramref name="country"/>, <paramref name="region"/> or <paramref name="city"/>
    /// is longer than <see cref="MaximumAreaNameLength"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// The <paramref name="postalCode"/> is longer than <see cref="MaximumPostalCodeLength"/>
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// The <paramref name="street"/> is longer than <see cref="MaximumStreetNameLength"/>.
    /// </exception>
    protected Address(
        string country,
        string? region,
        string? city,
        string? postalCode,
        string? street)
    {
        ThrowIfNullOrWhiteSpace(country);
        ThrowIfLongerThan(country, MaximumAreaNameLength);

        if (region is not null)
        {
            ThrowIfNullOrWhiteSpace(region);
            ThrowIfLongerThan(region, MaximumAreaNameLength);
        }

        if (city is not null)
        {
            ThrowIfNullOrWhiteSpace(city);
            ThrowIfLongerThan(city, MaximumAreaNameLength);
        }

        if (postalCode is not null)
        {
            ThrowIfNullOrWhiteSpace(postalCode);
            ThrowIfLongerThan(postalCode, MaximumPostalCodeLength);
            ThrowIfInvalidFormat(postalCode, PostalCodeFormatRegex);
        }

        if (street is not null)
        {
            ThrowIfNullOrWhiteSpace(street);
            ThrowIfLongerThan(street, MaximumStreetNameLength);
        }

        Country = country;
        Region = region;
        City = city;
        PostalCode = postalCode;
        Street = street;
    }
}
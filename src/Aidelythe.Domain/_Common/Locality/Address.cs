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
    public static readonly Regex PostalCodeFormatRegex = new(
        PostalCodeFormatPattern,
        options: RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.NonBacktracking,
        matchTimeout: TimeSpan.FromMilliseconds(100));

    // TODO: enforce rules

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
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="country"/> is null.
    /// </exception>
    protected Address(
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
namespace Aidelythe.Api._System.Localization;

/// <summary>
/// Contains supported culture names.
/// </summary>
public static class SupportedCultures
{
    private const string EnUs = "en-US";
    private const string RuRu = "ru-RU";

    /// <summary>
    /// Gets an array containing all supported cultures.
    /// </summary>
    public static string[] All { get; } =
    [
        EnUs,
        RuRu
    ];

    /// <summary>
    /// Gets the default culture.
    /// </summary>
    public static string Default => EnUs;
}
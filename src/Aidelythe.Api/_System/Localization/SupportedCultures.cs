using Aidelythe.Shared.Guards;

namespace Aidelythe.Api._System.Localization;

/// <summary>
/// Provides predefined constants and properties for supported cultures.
/// </summary>
public static class SupportedCultures
{
    /// <summary>
    /// The American English culture.
    /// </summary>
    public const string EnUs = "en-US";

    /// <summary>
    /// The Russian culture.
    /// </summary>
    public const string RuRu = "ru-RU";

    /// <summary>
    /// Gets an array containing all supported cultures.
    /// </summary>
    public static string[] All { get; } =
        typeof(SupportedCultures)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Select(fieldInfo => (string)fieldInfo
                .GetRawConstantValue()
                .ThrowIfNull())
            .ToArray();
}
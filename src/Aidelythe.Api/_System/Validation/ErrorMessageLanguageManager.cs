using Aidelythe.Api._Common.Validation.Resources;

namespace Aidelythe.Api._System.Validation;

/// <summary>
/// Represents a language manager that provides localized error messages.
/// </summary>
public sealed class ErrorMessageLanguageManager : ILanguageManager
{
    /// <inheritdoc/>
    /// <remarks>
    /// The member is not supported.
    /// </remarks>
    /// <exception cref="NotSupportedException">The member is accessed or set.</exception>
    public bool Enabled
    {
        get => throw BuildNotSupportedException(nameof(Enabled));
        set => throw BuildNotSupportedException(nameof(Enabled));
    }

    /// <inheritdoc/>
    /// <remarks>
    /// The member is not supported.
    /// </remarks>
    /// <exception cref="NotSupportedException">The member is accessed or set.</exception>
    public CultureInfo? Culture 
    { 
        get => throw BuildNotSupportedException(nameof(Culture));
        set => throw BuildNotSupportedException(nameof(Culture));
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException">The <paramref name="key"/> is null.</exception>
    /// <exception cref="KeyNotFoundException">The <paramref name="key"/> cannot be found.</exception>
    public string GetString(string key, CultureInfo? culture = null)
    {
        ThrowIfNull(key);

        var localizedErrorMessage = ValidationErrorMessages.ResourceManager.GetString(key, culture);

        return localizedErrorMessage ?? throw new KeyNotFoundException(
            $"The key '{key}' cannot be found in the resource file.");
    }

    private static NotSupportedException BuildNotSupportedException(string member)
    {
        return new NotSupportedException($"{member} is not supported by {nameof(ErrorMessageLanguageManager)}.");
    }
}
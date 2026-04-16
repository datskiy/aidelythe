using Aidelythe.Shared.RegularExpressions;

namespace Aidelythe.Api._Common.Sorting;

/// <summary>
/// Represents policies and constraints related to parsing and validating sorting parameters.
/// </summary>
public static class SortByPolicies
{
    /// <summary>
    /// The maximum acceptable length.
    /// </summary>
    public const int MaximumLength = 100;

    /// <summary>
    /// A regular expression pattern representing the sorting format.
    /// Ensures the parameter follows the specified structure of field names and sorting directions
    /// where field names consist of alphanumeric characters,
    /// and sorting directions are either "asc" or "desc".
    /// </summary>
    /// <example>
    /// fieldName1:asc,fieldName2:desc
    /// </example>
    public const string FormatPattern = @"(?i)^[a-z0-9]+:(asc|desc)(,[a-z0-9]+:(asc|desc))*$";

    /// <summary>
    /// A regular expression representing the sorting format.
    /// Ensures the parameter follows the specified structure of field names and sorting directions
    /// where field names consist of alphanumeric characters,
    /// and sorting directions are either "asc" or "desc".
    /// </summary>
    /// <example>
    /// fieldName1:asc,fieldName2:desc
    /// </example>
    public static readonly Regex FormatRegex = RegexHelper.CreateConfigured(FormatPattern);
}
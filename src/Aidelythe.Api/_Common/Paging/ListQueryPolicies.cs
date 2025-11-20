namespace Aidelythe.Api._Common.Paging;

/// <summary>
/// Provides policies and constraints for validating list query parameters.
/// </summary>
public static class ListQueryPolicies
{
    /// <summary>
    /// Represents policies and constraints related to validating the offset.
    /// </summary>
    public static class Offset
    {
        /// <summary>
        /// The minimum acceptable value.
        /// </summary>
        public const int MinimumValue = 0;
    }

    /// <summary>
    /// Represents policies and constraints related to validating the limit.
    /// </summary>
    public static class Limit
    {
        /// <summary>
        /// The minimum acceptable value.
        /// </summary>
        public const int MinimumValue = 1;

        /// <summary>
        /// The maximum acceptable value.
        /// </summary>
        public const int MaximumValue = 50;
    }

    /// <summary>
    /// Represents policies and constraints related to validating the search text.
    /// </summary>
    public static class SearchText
    {
        /// <summary>
        /// The maximum acceptable length.
        /// </summary>
        public const int MaximumLength = 100;
    }

    /// <summary>
    /// Represents policies and constraints related to validating the sorting.
    /// </summary>
    public static class SortBy
    {
        /// <summary>
        /// The maximum acceptable length.
        /// </summary>
        public const int MaximumLength = 100;

        /// <summary>
        /// A regular expression pattern representing the correct sorting format.
        /// Ensures the parameter follows the specified structure of field names and sorting directions
        /// where field names consist of alphanumeric characters,
        /// and sorting directions are either "asc" or "desc".
        /// </summary>
        /// <example>
        /// fieldName1:asc,fieldName2:desc
        /// </example>
        public static readonly Regex FormatRegex = new(
            pattern: "(?i)^[a-z0-9]+:(asc|desc)(,[a-z0-9]+:(asc|desc))*$",
            options: RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.NonBacktracking,
            matchTimeout: TimeSpan.FromMilliseconds(100));
    }
}
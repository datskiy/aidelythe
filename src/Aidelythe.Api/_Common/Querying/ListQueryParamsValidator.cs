using Aidelythe.Api._Common.Sorting;
using Aidelythe.Api._Common.Validation.Resources;
using Aidelythe.Application._Common.Filtering;
using Aidelythe.Application._Common.Paging;
using Aidelythe.Shared.Guards;

namespace Aidelythe.Api._Common.Querying;

/// <summary>
/// Represents a base validator for list query parameters.
/// </summary>
/// <typeparam name="TQueryParams"> The type of query parameters being validated. </typeparam>
public abstract class ListQueryParamsValidator<TQueryParams> : AbstractValidator<TQueryParams>
    where TQueryParams : ListQueryParams
{
    /// <summary>
    /// Gets a dictionary containing sortable field keys mapped to their corresponding property names.
    /// </summary>
    protected abstract IReadOnlyDictionary<string, string> SortableFieldsDictionary { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ListQueryParamsValidator{TQueryParams}"/> class.
    /// </summary>
    protected ListQueryParamsValidator()
    {
        RuleFor(queryParams => queryParams.Offset)
            .GreaterThanOrEqualTo(PagingScheme.MinimumOffsetValue);

        RuleFor(queryParams => queryParams.Limit)
            .InclusiveBetween(PagingScheme.MinimumLimitValue, PagingScheme.MaximumLimitValue);

        RuleFor(queryParams => queryParams.SearchText)
            .MaximumLength(FilteringScheme.MaximumSearchTextLength)
            .When(queryParams => queryParams.SearchText is not null);

        RuleFor(queryParams => queryParams.SortBy)
            .MaximumLength(SortByPolicies.MaximumLength)
            .Matches(SortByPolicies.FormatRegex)
            .DependentRules(() => ValidateSortedFields())
            .When(queryParams => queryParams.SortBy is not null);
    }

    private void ValidateSortedFields()
    {
        RuleFor(queryParams => queryParams.SortBy)
            .Must(str => str
                .ThrowIfNull()
                .Split(',')
                .Select(sortingRule => sortingRule.Split(':')[0])
                .GroupBy(sortedField => sortedField, StringComparer.OrdinalIgnoreCase)
                .All(group =>
                    group.Count() == 1 &&
                    SortableFieldsDictionary.ContainsKey(group.Key)))
            .WithMessage(_ => string.Format(
                ValidationErrorMessages.InvalidSortedFields,
                string.Join(", ", SortableFieldsDictionary.Keys)));
    }
}
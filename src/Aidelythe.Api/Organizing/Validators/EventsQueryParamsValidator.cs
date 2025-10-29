using Aidelythe.Api._Common.Sorting;
using Aidelythe.Api._Common.Validation;
using Aidelythe.Api.Organizing.Parameters;
using Aidelythe.Api.Organizing.SortingProviders;

namespace Aidelythe.Api.Organizing.Validators;

/// <summary>
/// Represents a validator for list query parameters for events.
/// </summary>
public sealed class EventsQueryParamsValidator : ListQueryParamsValidator<EventsQueryParams>
{
    /// <inheritdoc/>
    protected override IReadOnlyDictionary<string, string> SortableFieldsDictionary =>
        SortingRegistry<EventSortingProvider>.SortableFieldsDictionary;
}
using Aidelythe.Api._Common.Paging;
using Aidelythe.Api._Common.Sorting;

namespace Aidelythe.Api.Organizing.Events.Validators;

/// <summary>
/// Represents a validator for list query parameters for events.
/// </summary>
public sealed class EventsQueryParamsValidator : ListQueryParamsValidator<EventsQueryParams>
{
    /// <inheritdoc/>
    protected override IReadOnlyDictionary<string, string> SortableFieldsDictionary =>
        SortingRegistry<EventSortingProvider>.SortableFieldsDictionary;
}
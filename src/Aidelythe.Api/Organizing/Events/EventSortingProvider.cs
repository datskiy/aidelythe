using Aidelythe.Api._Common.Http.Serialization;
using Aidelythe.Api._Common.Sorting;
using Aidelythe.Api.Organizing.Events.Responses;
using Aidelythe.Application.Organizing.Events.Projections;

namespace Aidelythe.Api.Organizing.Events;

/// <summary>
/// Represents a sorting provider for events.
/// </summary>
public sealed class EventSortingProvider : SortingProvider
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EventSortingProvider"/> class.
    /// </summary>
    public EventSortingProvider() : base(
        BuildSortableFieldsDictionary())
    {
    }

    private static Dictionary<string, string> BuildSortableFieldsDictionary()
    {
        return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            {
                JsonPropertyNameHelper.TryResolve<EventSummaryResponse>(response => response.Id),
                nameof(EventSummary.Id)
            },
            {
                JsonPropertyNameHelper.TryResolve<EventSummaryResponse>(response => response.Title),
                nameof(EventSummary.Title)
            },
            // TODO: in the future remove this from sorting, add more useful fields
        };
    }
}
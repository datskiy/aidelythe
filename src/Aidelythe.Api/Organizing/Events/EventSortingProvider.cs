using Aidelythe.Api._Common.Http.Serialization;
using Aidelythe.Api.Organizing.Events.Responses;
using Aidelythe.Application.Organizing.Events.Projections;

namespace Aidelythe.Api.Organizing.Events;

/// <summary>
/// Provides a dictionary of sortable fields for events.
/// </summary>
public static class EventSortingProvider
{
    /// <summary>
    /// Gets a dictionary containing sortable field keys for events mapped to their corresponding property names.
    /// </summary>
    public static IReadOnlyDictionary<string, string> SortableFieldsDictionary =>
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            {
                JsonPropertyNameHelper.TryResolve<EventSummaryResponse>(response => response.Id),
                nameof(EventSummary.Id)
            },
            {
                JsonPropertyNameHelper.TryResolve<EventSummaryResponse>(response => response.Title),
                nameof(EventSummary.Title)
            },
            {
                JsonPropertyNameHelper.TryResolve<EventSummaryResponse>(response => response.StartsAt),
                nameof(EventSummary.StartsAt)
            },
            {
                JsonPropertyNameHelper.TryResolve<EventSummaryResponse>(response => response.EndsAt),
                nameof(EventSummary.EndsAt)
            },
            {
                JsonPropertyNameHelper.TryResolve<EventSummaryResponse>(response => response.CreatedAt),
                nameof(EventSummary.CreatedAt)
            },
            // TODO: in the future add more useful fields
            // TODO: deal with nested fields (e.g. Location.City)
        };
}
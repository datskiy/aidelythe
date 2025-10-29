using Aidelythe.Api._Common.Sorting;
using Aidelythe.Api.Organizing.Parameters;
using Aidelythe.Api.Organizing.Requests;
using Aidelythe.Api.Organizing.Responses;
using Aidelythe.Api.Organizing.SortingProviders;
using Aidelythe.Application._Common.Sorting;
using Aidelythe.Application.Organizing.Commands;
using Aidelythe.Application.Organizing.Projections;
using Aidelythe.Application.Organizing.Queries;
using Aidelythe.Shared.Collections;

namespace Aidelythe.Api.Organizing.Mappers;

/// <summary>
/// Provides mapping methods for events.
/// </summary>
[Mapper]
public static partial class EventMapper
{
    /// <summary>
    /// Maps the <see cref="EventsQueryParams"/> instance to a <see cref="GetEventsQuery"/> object.
    /// </summary>
    /// <param name="queryParams">The <see cref="EventsQueryParams"/> to map.</param>
    /// <returns>
    /// The mapped <see cref="GetEventsQuery"/>.
    /// </returns>
    public static GetEventsQuery ToQuery(this EventsQueryParams queryParams)
    {
        return new GetEventsQuery(
            queryParams.Offset,
            queryParams.Limit,
            queryParams.SearchText,
            queryParams.SortBy is null
                ? null
                : new SortingScheme(SortingHelper
                    .ParseRules(
                        queryParams.SortBy,
                        SortingRegistry<EventSortingProvider>.SortableFieldsDictionary)
                    .AsNonEmpty()));
    }

    /// <summary>
    /// Maps the <see cref="CreateEventRequest"/> instance to a <see cref="CreateEventCommand"/> object.
    /// </summary>
    /// <param name="request">The <see cref="CreateEventRequest"/> to map.</param>
    /// <returns>
    /// The mapped <see cref="CreateEventCommand"/>.
    /// </returns>
    public static partial CreateEventCommand ToCommand(this CreateEventRequest request);

    /// <summary>
    /// Maps the <see cref="UpdateEventRequest"/> instance to a <see cref="UpdateEventCommand"/> object.
    /// </summary>
    /// <param name="request">The <see cref="UpdateEventCommand"/> to map.</param>
    /// <param name="id">The unique identifier of the event.</param>
    /// <returns>
    /// The mapped <see cref="UpdateEventCommand"/>.
    /// </returns>
    public static partial UpdateEventCommand ToCommand(this UpdateEventRequest request, Guid id);

    /// <summary>
    /// Maps the <see cref="EventDetails"/> instance to a <see cref="EventDetailsResponse"/> object.
    /// </summary>
    /// <param name="eventDetails">The <see cref="EventDetails"/> to map.</param>
    /// <returns>
    /// The mapped <see cref="EventDetailsResponse"/>.
    /// </returns>
    public static partial EventDetailsResponse ToResponse(this EventDetails eventDetails);

    /// <summary>
    /// Maps the <see cref="EventSummary"/> instance to a <see cref="EventSummaryResponse"/> object.
    /// </summary>
    /// <param name="eventSummary">The <see cref="EventSummary"/> to map.</param>
    /// <returns>
    /// The mapped <see cref="EventSummaryResponse"/>.
    /// </returns>
    public static partial EventSummaryResponse ToResponse(this EventSummary eventSummary);
}
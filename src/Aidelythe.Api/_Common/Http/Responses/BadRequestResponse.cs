using Aidelythe.Api._Common.Http.Resources;
using Aidelythe.Shared.Collections;

namespace Aidelythe.Api._Common.Http.Responses;

/// <summary>
/// Represents a response for a 400 Bad Request error.
/// </summary>
public sealed class BadRequestResponse : ProblemDetailsResponse
{
    /// <summary>
    /// A dictionary for errors keyed by field name.
    /// </summary>
    [JsonPropertyName("errors")]
    public IDictionary<string, string[]> Errors { get; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="BadRequestResponse"/> class.
    /// </summary>
    /// <param name="validationFailures">The collection of validation failures.</param>
    /// <param name="traceId">A unique trace identifier that represents the request.</param>
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="validationFailures"/> or <paramref name="traceId"/> is null.
    /// </exception>
    public BadRequestResponse(
        INonEmptyCollection<ValidationFailure> validationFailures,
        string traceId) : base(
        StatusCodes.Status400BadRequest,
        ProblemTypeLinks.BadRequest,
            traceId)
    {
        ThrowIfNull(validationFailures);

        Errors = BuildErrorDictionary(validationFailures);
    }

    private static Dictionary<string, string[]> BuildErrorDictionary(
        INonEmptyCollection<ValidationFailure> validationFailures)
    {
        return validationFailures
            .GroupBy(failure => failure.PropertyName)
            .ToDictionary(
                grp => grp.Key,
                grp => grp
                    .Select(failure => failure.ErrorMessage)
                    .ToArray());
    }
}
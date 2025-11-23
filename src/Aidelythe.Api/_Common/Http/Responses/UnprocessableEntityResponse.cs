using Aidelythe.Api._Common.Http.Resources;

namespace Aidelythe.Api._Common.Http.Responses;

/// <summary>
/// Represents a response for a 422 Unprocessable Entity error.
/// </summary>
public sealed class UnprocessableEntityResponse : ProblemDetailsResponse
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnprocessableEntityResponse"/> class.
    /// </summary>
    /// <param name="detail">A human-readable explanation of the problem.</param>
    /// <param name="traceId">A unique trace identifier that represents the request.</param>
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="detail"/> or <paramref name="traceId"/> is null.
    /// </exception>
    public UnprocessableEntityResponse(
        string detail,
        string traceId) : base(
            ProblemTypeLinks.UnprocessableEntity,
            StatusCodes.Status422UnprocessableEntity,
            detail,
            traceId)
    {
    }
}
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
    /// <param name="traceId">A unique trace identifier that represents the request.</param>
    /// <param name="detail">A human-readable explanation of the problem.</param>
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="traceId"/> or <paramref name="detail"/> is null.
    /// </exception>
    public UnprocessableEntityResponse(
        string traceId,
        string detail) : base(
            StatusCodes.Status422UnprocessableEntity,
            ProblemTypeLinks.UnprocessableEntity,
            traceId,
            detail)
    {
    }
}
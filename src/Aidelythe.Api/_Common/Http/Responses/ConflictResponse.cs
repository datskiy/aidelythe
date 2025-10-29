using Aidelythe.Api._Common.Http.Resources;

namespace Aidelythe.Api._Common.Http.Responses;

/// <summary>
/// Represents a response for a 409 Conflict error.
/// </summary>
public sealed class ConflictResponse : ProblemDetailsResponse
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConflictResponse"/> class.
    /// </summary>
    /// <param name="traceId">A unique trace identifier that represents the request.</param>
    /// <param name="detail">A human-readable explanation of the problem.</param>
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="traceId"/> or <paramref name="detail"/> is null.
    /// </exception>
    public ConflictResponse(
        string traceId,
        string detail) : base(
        StatusCodes.Status409Conflict,
            ProblemTypeLinks.Conflict,
            traceId,
            detail)
    {
    }
}
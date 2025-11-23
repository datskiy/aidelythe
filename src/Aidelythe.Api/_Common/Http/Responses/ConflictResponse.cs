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
    /// <param name="detail">A human-readable explanation of the problem.</param>
    /// <param name="traceId">A unique trace identifier that represents the request.</param>
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="detail"/> or <paramref name="traceId"/> is null.
    /// </exception>
    public ConflictResponse(
        string detail,
        string traceId) : base(
            ProblemTypeLinks.Conflict,
            StatusCodes.Status409Conflict,
            detail,
            traceId)
    {
    }
}
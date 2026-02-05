using Aidelythe.Api._Common.Http.Resources;

namespace Aidelythe.Api._Common.Http.Responses;

/// <summary>
/// Represents a response for a 429 Too Many Requests error.
/// </summary>
public sealed class TooManyRequestsResponse : ProblemResponse
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TooManyRequestsResponse"/> class.
    /// </summary>
    /// <param name="traceId">A unique trace identifier that represents the request.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="traceId"/> is null.</exception>
    public TooManyRequestsResponse(string traceId) : base(
        ProblemTypeLinks.TooManyRequests,
        StatusCodes.Status429TooManyRequests,
        traceId)
    {
    }
}
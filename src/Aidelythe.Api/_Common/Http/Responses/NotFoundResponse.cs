using Aidelythe.Api._Common.Http.Resources;

namespace Aidelythe.Api._Common.Http.Responses;

/// <summary>
/// Represents a response for a 404 Not Found error.
/// </summary>
public sealed class NotFoundResponse : ProblemResponse
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundResponse"/> class.
    /// </summary>
    /// <param name="traceId">A unique trace identifier that represents the request.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="traceId"/> is null.</exception>
    public NotFoundResponse(string traceId) : base(
            ProblemTypeLinks.NotFound,
            StatusCodes.Status404NotFound,
            traceId)
    {
    }
}
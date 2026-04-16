using Aidelythe.Api._Common.Http.Resources;

namespace Aidelythe.Api._Common.Http.Responses;

/// <summary>
/// Represents a response for a 401 Unauthorized error.
/// </summary>
public sealed class UnauthorizedResponse : ProblemResponse
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnauthorizedResponse"/> class.
    /// </summary>
    /// <param name="traceId">A unique trace identifier that represents the request.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="traceId"/> is null.</exception>
    public UnauthorizedResponse(string traceId) : base(
        ProblemTypeLinks.Unauthorized,
        StatusCodes.Status401Unauthorized,
        traceId)
    {
    }
}
using Aidelythe.Api._Common.Http.Resources;

namespace Aidelythe.Api._Common.Http.Responses;

/// <summary>
/// Represents a response for a 403 Forbidden error.
/// </summary>
public sealed class ForbiddenResponse : ProblemResponse
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ForbiddenResponse"/> class.
    /// </summary>
    /// <param name="traceId">A unique trace identifier that represents the request.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="traceId"/> is null.</exception>
    public ForbiddenResponse(string traceId) : base(
        ProblemTypeLinks.Forbidden,
        StatusCodes.Status403Forbidden,
        traceId)
    {
    }
}
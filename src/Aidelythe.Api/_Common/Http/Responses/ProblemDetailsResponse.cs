namespace Aidelythe.Api._Common.Http.Responses;

/// <summary>
/// Represents a base response for problem details that follows
/// the machine-readable format specified in <see href="https://tools.ietf.org/html/rfc7807"/>.
/// </summary>
public abstract class ProblemDetailsResponse : ProblemDetails
{
    [JsonPropertyName("traceId")]
    public string TraceId { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ProblemDetailsResponse"/> class.
    /// </summary>
    /// <param name="status">The HTTP status code associated with this problem.</param>
    /// <param name="problemTypeLink">An absolute URI identifying the problem type.</param>
    /// <param name="traceId">A unique trace identifier that represents the request.</param>
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="problemTypeLink"/> or <paramref name="traceId"/> is null.
    /// </exception>
    protected ProblemDetailsResponse(
        int status,
        string problemTypeLink,
        string traceId)
    {
        ThrowIfNull(problemTypeLink);
        ThrowIfNull(traceId);

        Status = status;
        Type = problemTypeLink;
        TraceId = traceId;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ProblemDetailsResponse"/> class.
    /// </summary>
    /// <param name="status">The HTTP status code associated with this problem.</param>
    /// <param name="problemTypeLink">An absolute URI identifying the problem type.</param>
    /// <param name="traceId">A unique trace identifier that represents the request.</param>
    /// <param name="detail">A human-readable explanation of the problem.</param>
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="problemTypeLink"/>, <paramref name="traceId"/>, or <paramref name="detail"/> is null.
    /// </exception>
    protected ProblemDetailsResponse(
        int status,
        string problemTypeLink,
        string traceId,
        string detail) : this(
            status,
            problemTypeLink,
            traceId)
    {
        ThrowIfNull(detail);

        Detail = detail;
    }
}
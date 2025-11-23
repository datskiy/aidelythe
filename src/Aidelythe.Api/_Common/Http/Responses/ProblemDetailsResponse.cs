namespace Aidelythe.Api._Common.Http.Responses;

/// <summary>
/// Represents a base response for problem details that follows
/// the machine-readable format specified in <see href="https://tools.ietf.org/html/rfc7807"/>.
/// </summary>
public abstract class ProblemDetailsResponse : ProblemResponse
{
    /// <summary>
    /// Gets a human-readable explanation of the problem.
    /// </summary>
    [JsonPropertyOrder(-2)]
    [JsonPropertyName("detail")]
    public string Detail { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ProblemDetailsResponse"/> class.
    /// </summary>
    /// <param name="typeLink">An absolute URI identifying the problem type.</param>
    /// <param name="status">The HTTP status code associated with the problem.</param>
    /// <param name="detail">A human-readable explanation of the problem.</param>
    /// <param name="traceId">A unique trace identifier that represents the request.</param>
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="typeLink"/>, <paramref name="detail"/>, or <paramref name="traceId"/> is null.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">The <paramref name="status"/> is invalid.</exception>
    protected ProblemDetailsResponse(
        string typeLink,
        int status,
        string detail,
        string traceId) : base(
            typeLink,
            status,
            traceId)
    {
        ThrowIfNull(detail);

        Detail = detail;
    }
}
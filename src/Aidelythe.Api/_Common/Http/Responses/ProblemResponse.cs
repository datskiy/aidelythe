namespace Aidelythe.Api._Common.Http.Responses;

/// <summary>
/// Represents a base response for a problem that follows
/// the machine-readable format specified in <see href="https://tools.ietf.org/html/rfc7807"/>.
/// </summary>
public abstract class ProblemResponse
{
    /// <summary>
    /// Gets an absolute URI identifying the problem type.
    /// </summary>
    [JsonPropertyOrder(-5)]
    [JsonPropertyName("type")]
    public string TypeLink { get; }

    /// <summary>
    /// Gets a short, human-readable summary of the problem type.
    /// </summary>
    [JsonPropertyOrder(-4)]
    [JsonPropertyName("title")]
    public string Title { get; }

    /// <summary>
    /// Gets the HTTP status code associated with the problem.
    /// </summary>
    [JsonPropertyOrder(-3)]
    [JsonPropertyName("status")]
    public int Status { get; }

    /// <summary>
    /// Gets the unique trace identifier of the request.
    /// </summary>
    [JsonPropertyOrder(-1)]
    [JsonPropertyName("traceId")]
    public string TraceId { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ProblemResponse"/> class.
    /// </summary>
    /// <param name="typeLink">An absolute URI identifying the problem type.</param>
    /// <param name="status">The HTTP status code associated with the problem.</param>
    /// <param name="traceId">A unique trace identifier that represents the request.</param>
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="typeLink"/> or <paramref name="traceId"/> is null.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">The <paramref name="status"/> is invalid.</exception>
    protected ProblemResponse(
        string typeLink,
        int status,
        string traceId)
    {
        ThrowIfNull(typeLink);
        ThrowIfNull(traceId);

        TypeLink = typeLink;
        Status = status;
        TraceId = traceId;
        Title = ResolveStatusTitle(status);
    }

    private static string ResolveStatusTitle(int status)
    {
        return status switch
        {
            400 => "Bad Request",
            401 => "Unauthorized",
            403 => "Forbidden",
            404 => "Not Found",
            409 => "Conflict",
            422 => "Unprocessable Entity",
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, message: null)
        };
    }
}
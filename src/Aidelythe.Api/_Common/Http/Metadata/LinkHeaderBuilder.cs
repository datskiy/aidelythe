namespace Aidelythe.Api._Common.Http.Metadata;

/// <summary>
/// Represents a builder for Link header.
/// </summary>
public sealed class LinkHeaderBuilder
{
    private readonly HttpContext _httpContext;
    private readonly LinkGenerator _linkGenerator;

    private readonly string _controllerName;
    private readonly string _actionName;
    private readonly List<string> _links;

    /// <summary>
    /// Initializes a new instance of the <see cref="LinkHeaderBuilder"/> class.
    /// </summary>
    /// <param name="httpContext">The HTTP context associated with the current request.</param>
    /// <param name="linkGenerator">The link generator used to generate URI strings.</param>
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="httpContext"/> or <paramref name="linkGenerator"/> is null.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// The endpoint cannot be retrieved from the HTTP context,
    /// or the action descriptor cannot be retrieved from the endpoint metadata.
    /// </exception>
    public LinkHeaderBuilder(
        HttpContext httpContext,
        LinkGenerator linkGenerator)
    {
        ThrowIfNull(httpContext);
        ThrowIfNull(linkGenerator);

        _httpContext = httpContext;
        _linkGenerator = linkGenerator;

        var endpoint = httpContext.GetEndpoint();
        if (endpoint is null)
            throw new InvalidOperationException("The endpoint cannot be retrieved from the HTTP context.");

        var actionDescriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
        if (actionDescriptor is null)
            throw new InvalidOperationException("Action descriptor cannot be retrieved from the endpoint metadata.");

        _controllerName = actionDescriptor.ControllerName;
        _actionName = actionDescriptor.ActionName;
        _links = [];
    }

    /// <summary>
    /// Adds a link to the Link header.
    /// </summary>
    /// <param name="routeParams">The link route parameters for constructing the link.</param>
    /// <param name="rel">
    /// The relation type that describes the purpose of the link (e.g., "self", "next", "prev").
    /// </param>
    /// <returns>
    /// The current <see cref="LinkHeaderBuilder"/> instance with the new link added.
    /// </returns>
    /// <exception cref="ArgumentNullException">The <paramref name="rel"/> is null.</exception>
    /// <exception cref="InvalidOperationException">
    /// The link cannot be generated due to the action not being routable or missing required route parameters.
    /// </exception>
    public LinkHeaderBuilder Add(
        LinkRouteParams routeParams,
        string rel)
    {
        ThrowIfNull(rel);

        _links.Add($"<{TryComposeUri(routeParams, rel)}>; rel=\"{rel}\"");
        return this;
    }

    /// <summary>
    /// Conditionally adds a link to the Link header based on the specified condition.
    /// </summary>
    /// <param name="condition">A boolean value indicating whether to add the link.</param>
    /// <param name="routeParams">The link route parameters for constructing the link.</param>
    /// <param name="rel">
    /// The relation type that describes the purpose of the link (e.g., "self", "next", "prev").
    /// </param>
    /// <returns>
    /// The current <see cref="LinkHeaderBuilder"/> instance with the new link added.
    /// </returns>
    /// <exception cref="ArgumentNullException">The <paramref name="rel"/> is null.</exception>
    /// <exception cref="InvalidOperationException">
    /// The link cannot be generated due to the action not being routable or missing required route parameters.
    /// </exception>
    public LinkHeaderBuilder AddIf(
        bool condition,
        LinkRouteParams routeParams,
        string rel)
    {
        ThrowIfNull(rel);

        if (condition)
            Add(routeParams, rel);

        return this;
    }

    /// <summary>
    /// Builds and returns the Link header value as a single string composed of all the accumulated links.
    /// </summary>
    /// <returns>
    /// A string representation of the Link header containing all added links.
    /// </returns>
    public string Build()
    {
        return string.Join(", ", _links);
    }

    private string TryComposeUri(
        LinkRouteParams routeParams,
        string rel)
    {
        var uri = _linkGenerator.GetUriByAction(
            _httpContext,
            _actionName,
            _controllerName,
            values: routeParams);

        if (string.IsNullOrWhiteSpace(uri))
        {
            throw new InvalidOperationException(
                $"Failed to generate a pagination link for rel='{rel}'. " +
                "Verify that the action is routable and not missing required route parameters.");
        }

        return uri;
    }
}

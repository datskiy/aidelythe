namespace Aidelythe.Api._Common.Controllers.Http;

// TODO: refine + checks + desc all
// TODO: add exception on incorrect usage
public sealed class LinkHeaderBuilder
{
    private readonly HttpContext _httpContext;
    private readonly LinkGenerator _linkGenerator;

    private readonly string _controllerName;
    private readonly string _actionName;
    private readonly List<string> _links;

    public LinkHeaderBuilder(HttpContext httpContext, LinkGenerator linkGenerator)
    {
        _httpContext = httpContext;
        _linkGenerator = linkGenerator;

        var endpoint = httpContext.GetEndpoint();
        if(endpoint is null)
            throw new InvalidOperationException("Endpoint is null."); // TODO: refine

        var actionDescriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
        if (actionDescriptor is null)
            throw new InvalidOperationException("Action descriptor is null."); // TODO: refine

        _controllerName = actionDescriptor.ControllerName;
        _actionName = actionDescriptor.ActionName;
        _links = [];
    }

    public LinkHeaderBuilder Add(object routeValues, string rel)
    {
        _links.Add($"<{TryComposeUri(routeValues, rel)}>; rel=\"{rel}\"");
        return this;
    }

    public LinkHeaderBuilder AddIf(
        bool condition,
        object routeValues,
        string rel)
    {
        if (condition)
        {
            Add(routeValues, rel);
        }

        return this;
    }

    public string Build()
    {
        return string.Join(", ", _links);
    }

    private string TryComposeUri(object routeValues, string rel)
    {
        var uri = _linkGenerator.GetUriByAction(
            _httpContext,
            _actionName,
            _controllerName,
            routeValues);

        if (string.IsNullOrWhiteSpace(uri))
        {
            throw new InvalidOperationException(
                $"Failed to generate a pagination link for rel='{rel}'. " +
                "Verify that the action is routable and not missing required route parameters.");
        }

        return uri;
    }
}

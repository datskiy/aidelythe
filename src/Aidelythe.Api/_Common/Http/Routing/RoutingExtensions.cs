namespace Aidelythe.Api._Common.Http.Routing;

/// <summary>
/// Provides extension methods for routing.
/// </summary>
public static class RoutingExtensions
{
    private const string ControllerSuffix = "Controller";

    /// <summary>
    /// Removes the "Controller" suffix from a controller type name, if present.
    /// </summary>
    /// <exception cref="ArgumentException">
    /// The <paramref name="controllerName"/> is null, empty, or consists only of white-space characters.
    /// </exception>
    public static string RemoveControllerSuffix(this string controllerName)
    {
        ThrowIfNullOrWhiteSpace(controllerName);

        return controllerName.EndsWith(ControllerSuffix, StringComparison.OrdinalIgnoreCase)
            ? controllerName[..^ControllerSuffix.Length]
            : controllerName;
    }
}
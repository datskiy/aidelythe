namespace Aidelythe.Api._System.Telemetry.Logging;

/// <summary>
/// Provides extension methods for configuring logging middleware in the web application.
/// </summary>
public static class WebApplicationExtensions
{
    /// <summary>
    /// Adds the logging middleware to the web application to enrich
    /// the logging context with request-specific properties.
    /// </summary>
    /// <param name="app">The web application to configure.</param>
    /// <returns>
    /// The configured web application with the logging middleware added.
    /// </returns>
    /// <exception cref="ArgumentNullException">The <paramref name="app"/> is null.</exception>
    public static IApplicationBuilder UseRequestLogContext(this WebApplication app)
    {
        ThrowIfNull(app);

        return app.UseMiddleware<RequestLogContextMiddleware>();
    }
}
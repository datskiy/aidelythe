namespace Aidelythe.Api._System.Telemetry.Logging;

/// <summary>
/// Provides extension methods for configuring logging middleware in the application.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Adds the logging middleware to the specified application builder to enrich
    /// the logging context with request-specific properties.
    /// </summary>
    /// <param name="app">The application builder to configure.</param>
    /// <returns>
    /// The configured application with the logging middleware added.
    /// </returns>
    /// <exception cref="ArgumentNullException">The <paramref name="app"/> is null.</exception>
    public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder app)
    {
        ThrowIfNull(app);

        return app.UseMiddleware<RequestLogContextMiddleware>();
    }
}
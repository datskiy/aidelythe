using Aidelythe.Application._System.Authentication.Core;

namespace Aidelythe.Api._System.Monitoring;

/// <summary>
/// Provides extension methods for configuring health check middleware in the web application.
/// </summary>
public static class WebApplicationExtensions
{
    /// <summary>
    /// Adds the health check endpoint to the web application.
    /// </summary>
    /// <param name="app">The web application to configure.</param>
    /// <returns>
    /// The configured web application with the health check endpoint added.
    /// </returns>
    /// <exception cref="ArgumentNullException">The <paramref name="app"/> is null.</exception>
    public static IApplicationBuilder MapHealthChecks(this WebApplication app)
    {
        ThrowIfNull(app);

        app
            .MapHealthChecks("/health")
            .RequireAuthorization(policy => policy.RequireRole(
                AppRoles.System,
                AppRoles.Administrator));

        return app;
    }
}
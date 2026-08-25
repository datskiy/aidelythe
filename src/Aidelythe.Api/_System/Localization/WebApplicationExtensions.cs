namespace Aidelythe.Api._System.Localization;

/// <summary>
/// Provides extension methods for configuring localization middleware in the web application.
/// </summary>
public static class WebApplicationExtensions
{
    /// <summary>
    /// Adds the localization middleware to the web application to automatically set
    /// culture information limited to supported cultures.
    /// </summary>
    /// <param name="app">The web application to configure.</param>
    /// <returns>
    /// The configured web application with the localization middleware added.
    /// </returns>
    /// <exception cref="ArgumentNullException">The <paramref name="app"/> is null.</exception>
    public static IApplicationBuilder UseLocalization(this WebApplication app)
    {
        ThrowIfNull(app);

        return app.UseRequestLocalization(options => options
            .AddSupportedCultures(SupportedCultures.All)
            .AddSupportedUICultures(SupportedCultures.All)
            .SetDefaultCulture(SupportedCultures.Default));
    }
}
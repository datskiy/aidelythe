namespace Aidelythe.Api._System.Localization;

/// <summary>
/// Provides extension methods for configuring localization middleware in the application.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Adds the localization middleware to the specified application builder to automatically set
    /// culture information limited to supported cultures for requests based on information provided by the client.
    /// </summary>
    /// <param name="app">The application builder to configure.</param>
    /// <returns>
    /// The configured application builder with the localization middleware added.
    /// </returns>
    /// <exception cref="ArgumentNullException">The <paramref name="app"/> is null.</exception>
    public static IApplicationBuilder UseLocalization(this IApplicationBuilder app)
    {
        ThrowIfNull(app);

        return app
            .UseRequestLocalization(options => options
            .AddSupportedCultures(SupportedCultures.All)
            .AddSupportedUICultures(SupportedCultures.All)
            .SetDefaultCulture(SupportedCultures.EnUs));
    }
}
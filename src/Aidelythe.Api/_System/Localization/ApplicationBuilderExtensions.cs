namespace Aidelythe.Api._System.Localization;

/// <summary>
/// Provides extension methods for configuring localization middleware in the application.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Adds the localization middleware to the specified <see cref="IApplicationBuilder"/> to automatically set
    /// culture information limited to supported cultures for requests based on information provided by the client.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> to configure.</param>
    /// <returns>
    /// The configured <see cref="IApplicationBuilder"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">The <paramref name="app"/> is null.</exception>
    public static IApplicationBuilder UseLocalization(this IApplicationBuilder app)
    {
        ThrowIfNull(app);

        return app.UseRequestLocalization(options => options
                .AddSupportedCultures(SupportedCultures.All)
                .AddSupportedUICultures(SupportedCultures.All)
                .SetDefaultCulture(SupportedCultures.EnUs));
    }
}
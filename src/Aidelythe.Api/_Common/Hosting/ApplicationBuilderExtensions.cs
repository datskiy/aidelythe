namespace Aidelythe.Api._Common.Hosting;

/// <summary>
/// Provides extension methods for the application builder.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Conditionally applies configuration to the specified <see cref="IApplicationBuilder"/>
    /// when the current environment is not <c>Development</c>.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> to configure.</param>
    /// <param name="configuration">
    /// The configuration to apply when the current environment is not <c>Development</c>.
    /// </param>
    /// <returns>
    /// The configured <see cref="IApplicationBuilder"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="app"/> or <paramref name="configuration"/> is null.
    /// </exception>
    public static IApplicationBuilder WhenNotDevelopment(
        this IApplicationBuilder app,
        Action<IApplicationBuilder> configuration)
    {
        ThrowIfNull(app);
        ThrowIfNull(configuration);

        var environment = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

        return app.UseWhen(
            _ => !environment.IsDevelopment(),
            configuration);
    }
}
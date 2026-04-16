namespace Aidelythe.Api._Common.Hosting;

/// <summary>
/// Provides extension methods for the application builder.
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// Invokes the specified configurator when the current environment is <c>Development</c>.
    /// </summary>
    /// <param name="builder">The web application builder to invoke configurator on.</param>
    /// <param name="configurator">
    /// The configurator to execute when the current environment is <c>Development</c>.
    /// </param>
    /// <returns>
    /// The web application builder with conditionally invoked configurator.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="builder"/> or <paramref name="configurator"/> is null.
    /// </exception>
    public static WebApplicationBuilder WhenDevelopment(
        this WebApplicationBuilder builder,
        Action configurator)
    {
        ThrowIfNull(builder);
        ThrowIfNull(configurator);

        if (builder.Environment.IsDevelopment())
            configurator();

        return builder;
    }

    /// <summary>
    /// Invokes the specified configurator when the current environment is not <c>Development</c>.
    /// </summary>
    /// <param name="builder">The web application builder to invoke configurator on.</param>
    /// <param name="configurator">
    /// The configurator to execute when the current environment is not <c>Development</c>.
    /// </param>
    /// <returns>
    /// The web application builder with conditionally invoked configurator.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="builder"/> or <paramref name="configurator"/> is null.
    /// </exception>
    public static WebApplicationBuilder WhenNotDevelopment(
        this WebApplicationBuilder builder,
        Action configurator)
    {
        ThrowIfNull(builder);
        ThrowIfNull(configurator);

        if (!builder.Environment.IsDevelopment())
            configurator();

        return builder;
    }
}
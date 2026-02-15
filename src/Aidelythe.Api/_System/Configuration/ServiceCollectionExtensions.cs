using Aidelythe.Api._Common.Configuration;
using Aidelythe.Infrastructure._Common.Settings;

namespace Aidelythe.Api._System.Configuration;

/// <summary>
/// Provides extension methods for configuring application configuration.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds configuration options to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection to add configuration options to.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <returns>
    /// The service collection with configuration options added.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="services"/> or <paramref name="configuration"/> is null.
    /// </exception>
    public static IServiceCollection AddOptions(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        ThrowIfNull(services);
        ThrowIfNull(configuration);

        services.AddConfiguredOptions<AccessTokenSettings>(
            configuration,
            ConfigurationSections.AccessToken);

        services.AddConfiguredOptions<RefreshTokenSettings>(
            configuration,
            ConfigurationSections.RefreshToken);

        services.AddConfiguredOptions<RateLimitingSettings>(
            configuration,
            ConfigurationSections.RateLimiting);

        return services;
    }

    private static void AddConfiguredOptions<TOptions>(
        this IServiceCollection services,
        IConfiguration configuration,
        string sectionName)
        where TOptions : class
    {
        services
            .AddOptions<TOptions>()
            .Bind(configuration.GetSection(sectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }
}
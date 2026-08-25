using Aidelythe.Api._Common.Configuration;
using Aidelythe.Shared.Guards;

namespace Aidelythe.Api._System.Monitoring;

/// <summary>
/// Provides extension methods for configuring health check services in the service collection.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds health check services to the service collection.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <returns>
    /// The service collection with health check services added.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="services"/> or <paramref name="configuration"/> is null.
    /// </exception>
    public static IServiceCollection AddHealthChecks(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        ThrowIfNull(services);
        ThrowIfNull(configuration);

        services
            .AddHealthChecks()
            .AddNpgSql( configuration
                .GetConnectionString(ConnectionStrings.Default)
                .ThrowIfNull());

        return services;
    }
}
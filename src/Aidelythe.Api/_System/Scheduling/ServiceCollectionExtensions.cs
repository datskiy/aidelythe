using Aidelythe.Api._Common.Configuration;

namespace Aidelythe.Api._System.Scheduling;

/// <summary>
/// Provides extension methods for configuring scheduling services in the service collection.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds Hangfire services to the service collection.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <returns>
    /// The service collection with Hangfire services added.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="services"/> or <paramref name="configuration"/> is null.
    /// </exception>
    public static IServiceCollection AddHangfire(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        ThrowIfNull(services);
        ThrowIfNull(configuration);

        return services
            .AddHangfire(cfg => cfg
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage(options => options
                    .UseNpgsqlConnection(configuration.GetConnectionString(ConnectionStrings.Default))))
            .AddHangfireServer();
    }
}
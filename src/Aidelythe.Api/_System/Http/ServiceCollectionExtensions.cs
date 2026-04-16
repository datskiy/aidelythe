using Aidelythe.Api._System.Validation;

namespace Aidelythe.Api._System.Http;

/// <summary>
/// Provides extension methods for configuring HTTP pipeline services in the service collection.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds HTTP pipeline services to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <returns>
    /// The service collection with HTTP pipeline services added.
    /// </returns>
    /// <exception cref="ArgumentNullException">The <paramref name="services"/> is null.</exception>
    public static IServiceCollection AddHttpPipeline(this IServiceCollection services)
    {
        ThrowIfNull(services);

        services.AddControllers(options =>
        {
            options.Filters.Add<ValidationFilter>();
        });

        return services;
    }
}
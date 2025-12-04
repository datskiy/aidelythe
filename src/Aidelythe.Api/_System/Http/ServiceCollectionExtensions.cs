using Aidelythe.Api._System.Validation;

namespace Aidelythe.Api._System.Http;

/// <summary>
/// Provides extension methods for configuring HTTP pipeline services in the service collection.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds HTTP pipeline services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <returns>
    /// The <see cref="IMvcBuilder"/> with HTTP pipeline services added.
    /// </returns>
    /// <exception cref="ArgumentNullException">The <paramref name="services"/> is null.</exception>
    public static IMvcBuilder AddHttpPipeline(this IServiceCollection services)
    {
        ThrowIfNull(services);

        return services.AddControllers(options =>
        {
            options.Filters.Add<ValidationFilter>();
        });
    }
}
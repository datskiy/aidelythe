namespace Aidelythe.Api._System.Orchestration;

/// <summary>
/// Provides extension methods for configuring orchestration services in the service collection.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds mediator services and handlers to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <returns>
    /// The <see cref="IServiceCollection"/> with mediator services and handlers added.
    /// </returns>
    /// <exception cref="ArgumentNullException">The <paramref name="services"/> is null.</exception>
    public static IServiceCollection AddMediator(this IServiceCollection services)
    {
        ThrowIfNull(services);

        return services.AddMediatR(
            configuration => configuration.RegisterServicesFromAssemblies(
                typeof(Api.AssemblyMarker).Assembly,
                typeof(Application.AssemblyMarker).Assembly));
    }
}
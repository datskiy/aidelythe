using Aidelythe.Api._Common.Http.Serialization;

namespace Aidelythe.Api._System.Validation;

/// <summary>
/// Provides extension methods for configuring validation services in the service collection.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds validation services to the specified <see cref="IServiceCollection"/>,
    /// and configures global validation options.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <returns>
    /// The <see cref="IServiceCollection"/> with validation services added and configured.
    /// </returns>
    /// <exception cref="ArgumentNullException">The <paramref name="services"/> is null.</exception>
    public static IServiceCollection AddValidation(this IServiceCollection services)
    {
        ThrowIfNull(services);

        services
            .Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true)
            .AddValidatorsFromAssemblyContaining<AssemblyMarker>();

        ValidatorOptions.Global.LanguageManager = new ErrorMessageLanguageManager();

        ValidatorOptions.Global.PropertyNameResolver = (_, memberInfo, _) =>
            JsonPropertyNameHelper.TryResolve(memberInfo);

        return services;
    }
}
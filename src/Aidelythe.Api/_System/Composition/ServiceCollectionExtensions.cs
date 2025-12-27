using Aidelythe.Application._Common.Persistence;
using Aidelythe.Application._System.Authentication.Repositories;
using Aidelythe.Application._System.Authentication.Services;
using Aidelythe.Infrastructure._System.Authentication.Repositories;
using Aidelythe.Infrastructure._System.Authentication.Services;
using Aidelythe.Infrastructure._System.Persistence;

namespace Aidelythe.Api._System.Composition;

/// <summary>
/// Provides extension methods for configuring composition services in the service collection.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds composition services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <returns>
    /// The <see cref="IServiceCollection"/> with composition services added.
    /// </returns>
    /// <exception cref="ArgumentNullException">The <paramref name="services"/> is null.</exception>
    public static IServiceCollection AddComposition(this IServiceCollection services)
    {
        ThrowIfNull(services);

        services.AddTransient<IPasswordService, PasswordService>();
        services.AddTransient<IAccessTokenService, AccessTokenService>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserCredentialsRepository, UserCredentialsRepository>();

        return services;
    }
}
namespace Aidelythe.Api._System.Specification;

/// <summary>
/// Provides extension methods for configuring specification services in the service collection.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds API specification services to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <returns>
    /// The service collection with API specification services added.
    /// </returns>
    /// <exception cref="ArgumentNullException">The <paramref name="services"/> is null.</exception>
    public static IServiceCollection AddApiSpecification(this IServiceCollection services)
    {
        ThrowIfNull(services);

        return services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer((document, _, _) =>
            {
                const string jwtBearerScheme = JwtBearerDefaults.AuthenticationScheme;

                document.Components ??= new OpenApiComponents();
                document.Components.SecuritySchemes[jwtBearerScheme] =
                    new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.Http,
                        Scheme = jwtBearerScheme,
                        BearerFormat = "JWT",
                        Description = "JWT Bearer authentication"
                    };

                document.SecurityRequirements.Add(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = jwtBearerScheme,
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        []
                    }
                });

                return Task.CompletedTask;
            });
        });
    }
}
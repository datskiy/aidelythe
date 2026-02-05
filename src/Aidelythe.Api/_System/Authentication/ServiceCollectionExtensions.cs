using Aidelythe.Api._Common.Http.Metadata;
using Aidelythe.Api._Common.Http.Responses;

namespace Aidelythe.Api._System.Authentication;

/// <summary>
/// Provides extension methods for configuring authentication services in the service collection.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds JWT authentication services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <returns>
    /// The <see cref="IServiceCollection"/> with JWT authentication services added.
    /// </returns>
    /// <exception cref="ArgumentNullException">The <paramref name="services"/> is null.</exception>
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
    {
        ThrowIfNull(services);

        // TODO: get from config as options
        var issuer = "Aidelythe";
        var audience = "Aidelythe";
        var signingKey = new byte[32];
        var clockSkew = TimeSpan.FromSeconds(30);

        services
            .AddAuthentication(options => options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(signingKey),
                    ClockSkew = clockSkew,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };

                options.Events = new JwtBearerEvents
                {
                    OnChallenge = OverrideUnauthorizedResponseAsync,
                    OnForbidden = OverrideForbiddenResponseAsync
                };
            });

        return services;
    }

    private static Task OverrideUnauthorizedResponseAsync(JwtBearerChallengeContext challengeContext)
    {
        challengeContext.HandleResponse();

        var response = challengeContext.Response;
        response.StatusCode = StatusCodes.Status401Unauthorized;

        response.Headers.Append(
            HttpHeaders.WwwAuthenticate,
            BuildAuthenticationHeaderValue(challengeContext));

        return response.WriteAsJsonAsync(
            new UnauthorizedResponse(challengeContext.HttpContext.TraceIdentifier),
            challengeContext.HttpContext.RequestAborted);
    }

    private static Task OverrideForbiddenResponseAsync(ForbiddenContext forbiddenContext)
    {
        var response = forbiddenContext.Response;
        response.StatusCode = StatusCodes.Status403Forbidden;

        return response.WriteAsJsonAsync(
            new ForbiddenResponse(forbiddenContext.HttpContext.TraceIdentifier),
            forbiddenContext.HttpContext.RequestAborted);
    }

    private static string BuildAuthenticationHeaderValue(JwtBearerChallengeContext challengeContext)
    {
        const string authenticationScheme = JwtBearerDefaults.AuthenticationScheme;

        var error = string.IsNullOrEmpty(challengeContext.Error)
            ? null
            : $" error=\"{challengeContext.Error}\"";

        var errorDescription = string.IsNullOrEmpty(challengeContext.ErrorDescription)
            ? null
            : $"{(error is null ? " " : ", ")}error_description=\"{challengeContext.ErrorDescription}\"";

        return $"{authenticationScheme}{error}{errorDescription}";
    }
}
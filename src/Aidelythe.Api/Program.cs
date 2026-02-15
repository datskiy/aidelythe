using Aidelythe.Api;
using Aidelythe.Api._Common.Hosting;
using Aidelythe.Api._System.Authentication;
using Aidelythe.Api._System.Bandwidth;
using Aidelythe.Api._System.Composition;
using Aidelythe.Api._System.Configuration;
using Aidelythe.Api._System.Http;
using Aidelythe.Api._System.Localization;
using Aidelythe.Api._System.Orchestration;
using Aidelythe.Api._System.Specification;
using Aidelythe.Api._System.Telemetry.Logging;
using Aidelythe.Api._System.Validation;

var envConfiguration = ConfigurationInitializer.InitializeForCurrentEnvironment();
LoggerInitializer.InitializeSerilog(envConfiguration);

try
{
    Log.Information("Application starting");

    var builder = WebApplication.CreateBuilder(args);

    var configuration = builder.Configuration;
    configuration.AddConfiguration(envConfiguration);
    builder.WhenDevelopment(() => configuration.AddUserSecrets<AssemblyMarker>());

    var services = builder.Services;
    services.AddOptions(configuration);
    services.AddComposition();
    services.AddHttpPipeline();
    services.AddJwtAuthentication(configuration);
    services.AddAuthorization();
    services.AddRateLimiting(configuration);
    services.AddValidation();
    services.AddMediator();
    services.AddSerilog();
    services.AddApiSpecification();

    var app = builder.Build();
    app.UseHttpsRedirection();
    app.UseLocalization();
    app.UseAuthentication();
    app.UseAuthorization();
    builder.WhenNotDevelopment(() => app.UseRateLimiter());
    app.UseRequestLogging();
    app.MapControllers();
    app.MapOpenApi();
    app.MapScalarApiReference();

    Log.Information("Application started");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
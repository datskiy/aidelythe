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

var configuration = ConfigurationInitializer.InitializeForCurrentEnvironment();
LoggerInitializer.InitializeSerilog(configuration);

try
{
    Log.Information("Application starting");

    var builder = WebApplication.CreateBuilder(args);
    builder.Configuration.AddConfiguration(configuration);

    var services = builder.Services;
    services.AddComposition();
    services.AddHttpPipeline();
    services.AddJwtAuthentication();
    services.AddAuthorization();
    services.AddRateLimiting();
    services.AddValidation();
    services.AddMediator();
    services.AddSerilog();
    services.AddApiSpecification();

    var app = builder.Build();
    app.UseHttpsRedirection();
    app.UseLocalization();
    app.UseAuthentication();
    app.UseAuthorization();
    app.WhenNotDevelopment(a => a.UseRateLimiter());
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
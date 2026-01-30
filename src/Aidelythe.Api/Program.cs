using Aidelythe.Api._System.Authentication;
using Aidelythe.Api._System.Composition;
using Aidelythe.Api._System.Configuration;
using Aidelythe.Api._System.Http;
using Aidelythe.Api._System.Localization;
using Aidelythe.Api._System.Orchestration;
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
    services.AddValidation();
    services.AddMediator();
    services.AddOpenApi();
    services.AddSerilog();

    var app = builder.Build();
    app.UseHttpsRedirection();
    app.UseLocalization();
    app.UseAuthentication();
    app.UseAuthorization();
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
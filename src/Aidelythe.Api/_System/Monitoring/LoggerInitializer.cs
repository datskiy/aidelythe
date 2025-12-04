namespace Aidelythe.Api._System.Monitoring;

/// <summary>
/// Provides methods for initializing logging.
/// </summary>
public static class LoggerInitializer
{
    /// <summary>
    /// Initializes the Serilog framework with settings from the provided configuration.
    /// </summary>
    /// <param name="configuration">The configuration containing Serilog settings.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="configuration"/> is null.</exception>
    public static void InitializeSerilog(IConfiguration configuration)
    {
        ThrowIfNull(configuration);

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
    }
}
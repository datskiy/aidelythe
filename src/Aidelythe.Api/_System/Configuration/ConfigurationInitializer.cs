namespace Aidelythe.Api._System.Configuration;

/// <summary>
/// Provides initializing methods for configuration.
/// </summary>
public static class ConfigurationInitializer
{
    private const string AppSettingsPath = "_System/Configuration/Settings";

    /// <summary>
    /// Initializes the application configuration based on the current environment.
    /// </summary>
    /// <returns>
    /// A configuration based on the current environment.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// The ASP .NET Core environment is not set.
    /// </exception>
    public static IConfiguration InitializeForCurrentEnvironment()
    {
        var currentEnvironment = Environment.GetEnvironmentVariable(EnvironmentNames.AspNetCoreEnvironment);
        if (currentEnvironment is null)
            throw new InvalidOperationException(
                $"{EnvironmentNames.AspNetCoreEnvironment} environment variable is not set.");

        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(
                $"{AppSettingsPath}/appsettings.json",
                optional: false,
                reloadOnChange: true)
            .AddJsonFile(
                $"{AppSettingsPath}/appsettings.{currentEnvironment}.json",
                optional: false,
                reloadOnChange: true)
            .Build();
    }
}
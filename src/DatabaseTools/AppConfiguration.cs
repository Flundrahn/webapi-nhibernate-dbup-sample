using Microsoft.Extensions.Configuration;

namespace DatabaseTools;

internal static class AppConfiguration
{
    internal static (WebApiOptions, DatabaseToolsOptions) Build()
    {
        string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
            ?? throw new InvalidOperationException("Environment name must be set to avoid ambiguity.");;

        // If duplicate values are found in configuration files, values last added will be used, e.g. appsettings.Development.json will override appsettings.json.
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
            .AddJsonFile("database-tools-appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"database-tools-appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
            .Build();

        var webApiOptions = configuration.Get<WebApiOptions>()
                            ?? throw new InvalidOperationException("Configuration binder returned null for some reason.");

        // We can bind values from specific sections in the configuration files
        var databaseToolsOptions = configuration.GetSection("SpecificSection")
                                                .Get<DatabaseToolsOptions>()
                                                ?? throw new InvalidOperationException("Configuration binder returned null for some reason.");

        return (webApiOptions, databaseToolsOptions);
    }
}

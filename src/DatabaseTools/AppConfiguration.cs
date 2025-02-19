using Microsoft.Extensions.Configuration;

namespace DatabaseTools;

internal static class AppConfiguration
{
    internal static (WebApiOptions, DatabaseToolsOptions) Build()
    {
        string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
            ?? throw new InvalidOperationException("Environment name must be set to avoid ambiguity.");;

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
            .AddJsonFile("database-tools-appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"database-tools-appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
            .Build();

        var webApiOptions = configuration.Get<WebApiOptions>()
                            ?? throw new InvalidOperationException("Configuration binder returned null for some reason.");

        var databaseToolsOptions = configuration.GetSection("SpecificSection")
                                                .Get<DatabaseToolsOptions>()
                                                ?? throw new InvalidOperationException("Configuration binder returned null for some reason.");

        return (webApiOptions, databaseToolsOptions);
    }
}

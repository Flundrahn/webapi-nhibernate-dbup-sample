using Microsoft.Extensions.Configuration;

namespace DatabaseTools;
public class AppOptions
{
    private string? _connectionString;
    public string ConnectionString
    {
        get
        {
            return string.IsNullOrWhiteSpace(_connectionString)
                ? throw new InvalidOperationException("ConnectionString is missing.")
                : _connectionString;
        }

        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidOperationException("ConnectionString cannot be whitespace.");
            }
            _connectionString = value;
        }
    }
    public bool AlwaysRollback { get; set; }
    public bool IsDevelopment { get; set; }
}

internal class AppConfiguration
{
    private static string EnvironmentName
    {
        get => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") 
            ?? throw new InvalidOperationException("Environment name must be set to avoid ambiguity.");
    }

    internal static AppOptions Build()
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{EnvironmentName}.json", optional: true, reloadOnChange: true)
            .Build();

        var appOptions = configuration.Get<AppOptions>()
                         ?? throw new NullReferenceException("Configuration binder returned null for some reason.");
        appOptions.IsDevelopment = string.Equals(EnvironmentName, "Development", StringComparison.OrdinalIgnoreCase);

        return appOptions;
    }
}

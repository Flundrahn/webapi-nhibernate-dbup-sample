using Shared.Exceptions;

namespace Shared;

public class AppOptions
{
    private string? _connectionString;
    public string ConnectionString
    {
        get
        {
            return string.IsNullOrWhiteSpace(_connectionString)
                ? throw new InvalidConfigurationException("ConnectionString is missing.")
                : _connectionString;
        }

        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidConfigurationException("ConnectionString cannot be whitespace.");
            }
            _connectionString = value;
        }
    }
    public string? Environment { get; set; }
    public bool IsDevelopment => Environment is null
        ? throw new InvalidConfigurationException("Environment name must be set to avoid ambiguity, check launchSettings.json.")
        : string.Equals(Environment, "Development", StringComparison.OrdinalIgnoreCase);
}

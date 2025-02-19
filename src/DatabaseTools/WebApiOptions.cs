namespace DatabaseTools;

public class WebApiOptions
{
    private string? _connectionString;
    public string ConnectionString
    {
        get
        {
            if (string.IsNullOrWhiteSpace(_connectionString))
            {
                throw new InvalidOperationException("ConnectionString is missing.");
            }
            return _connectionString;
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
    public string? Environment { get; set; }
    public bool IsDevelopment => string.Equals(Environment, "Development", StringComparison.OrdinalIgnoreCase);
}

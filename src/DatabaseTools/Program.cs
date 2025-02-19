using DatabaseTools;
using DbUp;
using DbUp.Builder;
using DbUp.Engine;
using DbUp.Support;

(WebApiOptions webApiOptions, DatabaseToolsOptions databaseToolsOptions) = AppConfiguration.Build();

UpgradeEngineBuilder builder = DeployChanges.To
    .SqlDatabase(webApiOptions.ConnectionString)
    .LogToConsole()
    // Here there are nice ways of configuring if script should only run once (as tracked by generated table dbo.SchemaVersions) or always.
    .WithScriptsFromFileSystem(Path.Combine(AppContext.BaseDirectory, "scripts\\migrations"),
                               new SqlScriptOptions { ScriptType = ScriptType.RunOnce, RunGroupOrder = 1 })
    .WithScriptsFromFileSystem(Path.Combine(AppContext.BaseDirectory, "scripts\\stored-procedures"),
                               new SqlScriptOptions { ScriptType = ScriptType.RunAlways, RunGroupOrder = 2 });

if (databaseToolsOptions.ShouldSeedDatabase)
{
    // Using RunGroupOrder we can ensure that these scripts always runs after potential migrations 
    builder.WithScriptsFromFileSystem(Path.Combine(AppContext.BaseDirectory, "scripts\\seed-data"),
                                      new SqlScriptOptions { ScriptType = ScriptType.RunAlways, RunGroupOrder = 3 });
}

if (databaseToolsOptions.AlwaysRollback)
{
    // Could be used for testing purposes.
    builder = builder.WithTransactionAlwaysRollback();
}
else
{
    // Also possible to use transaction per script.
    builder = builder.WithTransaction();
}

UpgradeEngine upgrader = builder.Build();

DatabaseUpgradeResult result = upgrader.PerformUpgrade();

if (!result.Successful)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(result.Error);
    Console.ResetColor();
    return -1;
}

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Success!");
Console.ResetColor();
return 0;
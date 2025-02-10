using DatabaseTools;
using DbUp;
using DbUp.Builder;
using DbUp.Engine;
using DbUp.Support;

AppOptions appOptions = AppConfiguration.Build();

UpgradeEngineBuilder builder = DeployChanges.To
    .SqlDatabase(appOptions.ConnectionString)
    .WithScriptsFromFileSystem("scripts\\migrations",
                               new SqlScriptOptions { ScriptType = ScriptType.RunOnce, RunGroupOrder = 1 })
    .WithScriptsFromFileSystem("scripts\\stored-procedures",
                               new SqlScriptOptions { ScriptType = ScriptType.RunAlways, RunGroupOrder = 2 })
    .LogToConsole();

if (appOptions.AlwaysRollback)
{
    builder = builder.WithTransactionAlwaysRollback();
}
else
{
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
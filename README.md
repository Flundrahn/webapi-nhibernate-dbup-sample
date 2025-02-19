# 

Small sample project to experiment with `DbUp`, `FluentNHibernate`, `Microsoft.Extensions.Configuration`.
There are two apps, an ASP.NET Core WebApi and a DbUp console project called DatabaseTools to manage the database migrations and more.
The appsettings.json file in the WebApi project is used in both WebApi and DatabaseTools, connection string only needs to be configured in one place.
The different settings jsons will bind to strongly typed configuration classes in respective projects.
Another interesting thing is using FluentNHibernate to map the entity instead of hbm.xml:s, see `DocumentMap.cs`.

## Prerequisites

- .NET 9 SDK
- SQL Server

## Installation

1. Create a database in SQL Server, see connectionstring default in `src/WebApi/appsettings.json/`
2. Build and run DatabaseTools to create database schema:

- Optionally create local settings file `database-tools-appsettings.Development.json` and set ShouldSeedDatabase to true to add some test data.

```sh
 dotnet run --project .\src\DatabaseTools\
```

3. Build and run WebApi project

```sh
dotnet run --project .\src\WebApi\ --launch-profile https
```

4. Browse to `https://localhost:7291/swagger` (default applicationUrl, see `launchSettings.json`) to test the API.

## NHibernate ExportSchema

It is also possible to set `ShouldExportDatabaseSchema` to true in `appsettings.json` to generate the DB schema.
This will cause NHibernate to drop any existing tables for mapped entities and recreate them using configured mappings.
It could be a convenient way to test if the mapping matches with the table created using migrations.

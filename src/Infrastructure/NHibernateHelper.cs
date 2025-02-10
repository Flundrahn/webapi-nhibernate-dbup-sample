﻿using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Infrastructure.Mappings;
using NHibernate;
using NHibernate.Driver;
using Shared;

namespace Infrastructure;

/// <summary>
/// Class that should be internal to infrastructure layer, to encapsulate the implementation details of ORM and DB.
/// </summary>
internal class NHibernateHelper
{
    private readonly string _connectionString;
    private readonly bool _isDevelopment;

    internal FluentConfiguration Configuration { get; }

    internal NHibernateHelper(AppOptions appOptions)
    {
        if (appOptions.ConnectionString == string.Empty)
        {
            throw new InvalidOperationException("Connection string cannot be empty.");
        }

        _connectionString = appOptions.ConnectionString;
        _isDevelopment = appOptions.IsDevelopment;
        Configuration = CreateConfiguration();
    }

    private FluentConfiguration CreateConfiguration()
    {
        var databaseConfig = MsSqlConfiguration.MsSql2012
            .ConnectionString(_connectionString)
            .Driver<MicrosoftDataSqlClientDriver>();

        if (_isDevelopment)
        {
            databaseConfig = databaseConfig.ShowSql();
        }

        var configuration = Fluently.Configure()
            .Database(databaseConfig)
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<DocumentMap>());

        return configuration;
    }

    internal ISessionFactory CreateSessionFactory()
    {
        return Configuration.BuildSessionFactory();
    }
}

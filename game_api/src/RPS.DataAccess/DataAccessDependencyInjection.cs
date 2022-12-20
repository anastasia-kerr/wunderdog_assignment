﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RPS.DataAccess.Persistence;
using RPS.DataAccess.Repositories;
using RPS.Core.Interfaces;

namespace RPS.DataAccess;

public static class DataAccessDependencyInjection
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);

        services.AddRepositories();

        return services;
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IGameRepository, GameRepository>();

    }

    private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseConfig = configuration.GetSection("Database").Get<DatabaseConfiguration>();

        if (databaseConfig.UseInMemoryDatabase)
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseInMemoryDatabase("RPS");
                options.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });
        else
            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(databaseConfig.ConnectionString,
                    opt => opt.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName)));
    }
}

// TODO move outside?
public class DatabaseConfiguration
{
    public bool UseInMemoryDatabase { get; set; }

    public string ConnectionString { get; set; }
}

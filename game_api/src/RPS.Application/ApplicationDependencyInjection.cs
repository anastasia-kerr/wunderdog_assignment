using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using RPS.Application.Services;
using RPS.Application.Validators;

namespace RPS.Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IWebHostEnvironment env)
    {
        services.AddServices(env);

        return services;
    }

    private static void AddServices(this IServiceCollection services, IWebHostEnvironment env)
    {
        services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<ISystemService, SystemService>();
        services.AddSingleton<ISystemStateValidator, RedStateValidator>();
        services.AddSingleton<ISystemStateValidator, AmberStateValidator>();
        services.AddSingleton<ISystemStateValidator, GreenStateValidator>();
    }
}

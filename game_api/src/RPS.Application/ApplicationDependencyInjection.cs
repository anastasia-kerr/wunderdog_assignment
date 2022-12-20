using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using RPS.Application.Services;
using RPS.Application.MappingProfiles;

namespace RPS.Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IWebHostEnvironment env)
    {
        services.AddServices(env);

        services.RegisterAutoMapper();

        return services;
    }

    private static void AddServices(this IServiceCollection services, IWebHostEnvironment env)
    {
        services.AddScoped<IGamePlayService, GamePlayService>();
        services.AddScoped<IPlayersService, PlayersService>();
    }

    private static void RegisterAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(IMappingProfilesMarker));
    }
}

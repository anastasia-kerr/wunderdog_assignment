namespace RPS.API;

public static class ApiDependencyInjection
{
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen();
    }
}

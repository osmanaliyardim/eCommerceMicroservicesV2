using eCommerceMicroservicesV2.BuildingBlocks.Exceptions.Handler;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace eCommerceMicroservicesV2.Ordering.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        var connStr = configuration.GetConnectionString(Messages.ORDERING_DB_NAME);

        services.AddCarter();

        services.AddExceptionHandler<CustomExceptionHandler>();

        services.AddHealthChecks()
                .AddSqlServer(connStr!);

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.MapCarter();

        app.UseExceptionHandler(options => { });

        app.UseHealthChecks(Messages.HEALTH_CHECK_ENDPOINT,
            new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

        return app;
    }
}

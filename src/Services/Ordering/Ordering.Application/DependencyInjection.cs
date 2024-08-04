using eCommerceMicroservicesV2.BuildingBlocks.Behaviors;
using eCommerceMicroservicesV2.BuildingBlocks.Messaging.MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Microsoft.FeatureManagement;

namespace eCommerceMicroservicesV2.Ordering.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });

        services.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());

        services.AddFeatureManagement();

        return services;
    }
}

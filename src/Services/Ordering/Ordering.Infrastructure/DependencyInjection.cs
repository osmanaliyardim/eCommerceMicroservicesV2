using eCommerceMicroservicesV2.Ordering.Infrastructure.Data;
using eCommerceMicroservicesV2.Ordering.Infrastructure.Data.Interceptors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerceMicroservicesV2.Ordering.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connStr = configuration.GetConnectionString(Messages.ORDERING_DB_NAME);

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.AddInterceptors(new AuditableEntityInterceptor());
            options.UseSqlServer(connStr);
        });

        return services;
    }
}

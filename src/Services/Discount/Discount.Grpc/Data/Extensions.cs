using Microsoft.EntityFrameworkCore;

namespace eCommerceMicroservicesV2.Discount.Grpc.Data;

public static class Extensions
{
    public async static Task<IApplicationBuilder> UseMigrationAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<DiscountContext>();

        await dbContext.Database.MigrateAsync();

        return app;
    }
}

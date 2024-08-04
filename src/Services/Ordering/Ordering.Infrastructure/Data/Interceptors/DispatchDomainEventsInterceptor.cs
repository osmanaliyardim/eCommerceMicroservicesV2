using eCommerceMicroservicesV2.Ordering.Domain.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace eCommerceMicroservicesV2.Ordering.Infrastructure.Data.Interceptors;

public class DispatchDomainEventsInterceptor(IMediator mediator) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges
        (DbContextEventData eventData, InterceptionResult<int> result)
    {
        DispatchDomainEventsAsync(eventData.Context).GetAwaiter().GetResult();

        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync
        (DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        await DispatchDomainEventsAsync(eventData.Context);

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private async Task DispatchDomainEventsAsync(DbContext? context)
    {
        if (context == null) return;

        var aggregates = context.ChangeTracker
            .Entries<IAggregate>()
                .Where(agg => agg.Entity.DomainEvents.Any())
                    .Select(agg => agg.Entity);

        var domainEvents = aggregates
                .SelectMany(agg => agg.DomainEvents)
                    .ToList();

        aggregates
            .ToList()
                .ForEach(agg => agg.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);
    }
}

using eCommerceMicroservicesV2.Ordering.Application.Extensions;

namespace eCommerceMicroservicesV2.Ordering.Application.Orders.Queries.GetOrderByName;

public class GetOrdersByNameHandler(IApplicationDbContext context)
    : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
{
    public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
    {
        var orderName = OrderName.Of(query.Name);
        
        var orders = await context.Orders
            .Include(o => o.OrderItems)
                .Where(o => o.OrderName.Value.Contains(query.Name))
                    .OrderBy(o => o.OrderName.Value)
                        .AsNoTracking()
                            .ToListAsync(cancellationToken);

        return new GetOrdersByNameResult(orders.ToOrderDtoList());
    }
}

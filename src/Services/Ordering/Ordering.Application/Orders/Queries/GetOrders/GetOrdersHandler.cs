using eCommerceMicroservicesV2.Ordering.Application.Extensions;

namespace eCommerceMicroservicesV2.Ordering.Application.Orders.Queries.GetOrders;

public class GetOrdersHandler(IApplicationDbContext context)
    : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;
        var totalCount = await context.Orders.LongCountAsync(cancellationToken);
        
        var orders = await context.Orders
            .Include(o => o.OrderItems)
                .OrderBy(o => o.OrderName.Value)
                    .Skip(pageIndex * pageSize)
                        .Take(pageSize)
                            .AsNoTracking()
                                .ToListAsync(cancellationToken);

        var paginatedOrders = new PaginatedResult<OrderDto>
            (pageIndex, pageSize, totalCount, orders.ToOrderDtoList());

        return new GetOrdersResult(paginatedOrders);
    }
}
